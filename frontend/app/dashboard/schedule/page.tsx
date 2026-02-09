"use client"

import { useState, useMemo } from "react"
import { ChevronLeft, ChevronRight, Plus } from "lucide-react"
import { Card, CardContent } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { cn } from "@/lib/utils"

const DAYS = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"]

const EVENT_TYPES = [
  { name: "Meeting", color: "bg-primary" },
  { name: "Operations", color: "bg-primary" },
  { name: "Lunch", color: "bg-primary" },
  { name: "Conference", color: "bg-primary" },
  { name: "Business Meeting", color: "bg-primary" },
]

type ViewMode = "month" | "week" | "day"

function getDaysInMonth(year: number, month: number) {
  return new Date(year, month + 1, 0).getDate()
}

function getFirstDayOfMonth(year: number, month: number) {
  return new Date(year, month, 1).getDay()
}

export default function SchedulePage() {
  const today = new Date()
  const [currentDate, setCurrentDate] = useState(new Date(today.getFullYear(), today.getMonth(), 1))
  const [viewMode, setViewMode] = useState<ViewMode>("month")

  const year = currentDate.getFullYear()
  const month = currentDate.getMonth()
  const daysInMonth = getDaysInMonth(year, month)
  const firstDay = getFirstDayOfMonth(year, month)

  const prevMonthDays = getDaysInMonth(year, month - 1)

  const calendarDays = useMemo(() => {
    const days: { day: number; currentMonth: boolean; isToday: boolean }[] = []

    // Previous month days
    for (let i = firstDay - 1; i >= 0; i--) {
      days.push({
        day: prevMonthDays - i,
        currentMonth: false,
        isToday: false,
      })
    }

    // Current month days
    for (let i = 1; i <= daysInMonth; i++) {
      const isToday =
        i === today.getDate() &&
        month === today.getMonth() &&
        year === today.getFullYear()
      days.push({ day: i, currentMonth: true, isToday })
    }

    // Next month days
    const remaining = 42 - days.length
    for (let i = 1; i <= remaining; i++) {
      days.push({ day: i, currentMonth: false, isToday: false })
    }

    return days
  }, [year, month, daysInMonth, firstDay, prevMonthDays, today])

  const monthName = currentDate.toLocaleString("default", { month: "long" })

  const goToPrev = () => {
    setCurrentDate(new Date(year, month - 1, 1))
  }

  const goToNext = () => {
    setCurrentDate(new Date(year, month + 1, 1))
  }

  const goToToday = () => {
    setCurrentDate(new Date(today.getFullYear(), today.getMonth(), 1))
  }

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Schedule Timing</h1>
        <p className="text-sm text-muted-foreground">
          Manage your calendar and events
        </p>
      </div>

      <div className="flex flex-col gap-6 lg:flex-row">
        {/* Left sidebar - Events */}
        <Card className="w-full border-border shadow-sm lg:w-56 lg:shrink-0">
          <CardContent className="p-4">
            <h3 className="mb-4 text-sm font-semibold text-foreground">All Events</h3>
            <div className="flex flex-wrap gap-2 lg:flex-col">
              {EVENT_TYPES.map((event) => (
                <Badge
                  key={event.name}
                  className={cn(
                    "cursor-grab px-3 py-1.5 text-xs font-medium text-primary-foreground",
                    event.color
                  )}
                >
                  {event.name}
                </Badge>
              ))}
            </div>
            <label className="mt-6 flex items-center gap-2 text-sm text-muted-foreground">
              <input type="checkbox" className="h-4 w-4 rounded border-border accent-primary" />
              Remove after drop
            </label>
          </CardContent>
        </Card>

        {/* Calendar */}
        <Card className="flex-1 border-border shadow-sm">
          <CardContent className="p-4">
            {/* Toolbar */}
            <div className="mb-4 flex flex-wrap items-center justify-between gap-3">
              <div className="flex items-center gap-2">
                <div className="flex overflow-hidden rounded-lg border border-border">
                  <button
                    onClick={goToPrev}
                    className="bg-primary px-3 py-2 text-primary-foreground transition-opacity hover:opacity-90"
                    aria-label="Previous month"
                  >
                    <ChevronLeft className="h-4 w-4" />
                  </button>
                  <button
                    onClick={goToNext}
                    className="bg-primary px-3 py-2 text-primary-foreground transition-opacity hover:opacity-90"
                    aria-label="Next month"
                  >
                    <ChevronRight className="h-4 w-4" />
                  </button>
                </div>
                <button
                  onClick={goToToday}
                  className="rounded-lg border border-border bg-card px-4 py-2 text-sm font-medium text-muted-foreground transition-colors hover:bg-secondary hover:text-foreground"
                >
                  Today
                </button>
                <button className="flex items-center gap-1 rounded-lg bg-primary px-4 py-2 text-sm font-medium text-primary-foreground transition-opacity hover:opacity-90">
                  <Plus className="h-4 w-4" />
                  Add Event
                </button>
              </div>

              <h2 className="text-xl font-bold text-foreground">
                {monthName} {year}
              </h2>

              <div className="flex overflow-hidden rounded-lg border border-primary">
                {(["month", "week", "day"] as ViewMode[]).map((mode) => (
                  <button
                    key={mode}
                    onClick={() => setViewMode(mode)}
                    className={cn(
                      "px-4 py-2 text-sm font-medium capitalize transition-colors",
                      viewMode === mode
                        ? "bg-primary text-primary-foreground"
                        : "bg-card text-muted-foreground hover:text-foreground"
                    )}
                  >
                    {mode}
                  </button>
                ))}
              </div>
            </div>

            {/* Calendar Grid */}
            <div className="overflow-hidden rounded-lg border border-border">
              {/* Day headers */}
              <div className="grid grid-cols-7 bg-primary">
                {DAYS.map((day) => (
                  <div
                    key={day}
                    className="px-2 py-3 text-center text-sm font-semibold text-primary-foreground"
                  >
                    {day}
                  </div>
                ))}
              </div>

              {/* Day cells */}
              <div className="grid grid-cols-7">
                {calendarDays.map((d, i) => (
                  <div
                    key={i}
                    className={cn(
                      "relative min-h-[80px] border-b border-r border-border p-2 transition-colors lg:min-h-[100px]",
                      !d.currentMonth && "bg-secondary/30",
                      d.isToday && "bg-amber-50"
                    )}
                  >
                    <span
                      className={cn(
                        "text-sm font-medium",
                        d.currentMonth ? "text-primary" : "text-muted-foreground/50"
                      )}
                    >
                      {d.day}
                    </span>
                  </div>
                ))}
              </div>
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}
