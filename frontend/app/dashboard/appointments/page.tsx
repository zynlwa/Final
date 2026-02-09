"use client"

import { useState } from "react"
import { Search, Filter } from "lucide-react"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { cn } from "@/lib/utils"

const appointments = [
  {
    id: 1,
    patient: "Christopher Burrell",
    age: "25 Years",
    type: "Cardiogram",
    date: "13 Mar 2026",
    time: "09:00 AM",
    status: "Approved",
    avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=80&h=80&fit=crop&crop=face",
  },
  {
    id: 2,
    patient: "Sarah Johnson",
    age: "32 Years",
    type: "Checkup",
    date: "14 Mar 2026",
    time: "10:30 AM",
    status: "Pending",
    avatar: "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=80&h=80&fit=crop&crop=face",
  },
  {
    id: 3,
    patient: "Michael Smith",
    age: "45 Years",
    type: "Covid Test",
    date: "15 Mar 2026",
    time: "01:00 PM",
    status: "Approved",
    avatar: "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=80&h=80&fit=crop&crop=face",
  },
  {
    id: 4,
    patient: "Emily Davis",
    age: "28 Years",
    type: "Dentist",
    date: "16 Mar 2026",
    time: "03:30 PM",
    status: "Canceled",
    avatar: "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=80&h=80&fit=crop&crop=face",
  },
  {
    id: 5,
    patient: "David Wilson",
    age: "38 Years",
    type: "Orthopedic",
    date: "17 Mar 2026",
    time: "11:00 AM",
    status: "Approved",
    avatar: "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=80&h=80&fit=crop&crop=face",
  },
]

function getStatusClasses(status: string) {
  switch (status) {
    case "Approved":
      return "bg-emerald-100 text-emerald-700"
    case "Pending":
      return "bg-amber-100 text-amber-700"
    case "Canceled":
      return "bg-red-100 text-red-700"
    default:
      return "bg-secondary text-secondary-foreground"
  }
}

export default function AppointmentsPage() {
  const [search, setSearch] = useState("")

  const filteredAppointments = appointments.filter((a) =>
    a.patient.toLowerCase().includes(search.toLowerCase()) ||
    a.type.toLowerCase().includes(search.toLowerCase())
  )

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Appointments</h1>
        <p className="text-sm text-muted-foreground">
          Manage patient appointments
        </p>
      </div>

      <Card className="border-border shadow-sm">
        <CardHeader className="flex flex-row items-center justify-between pb-3">
          <CardTitle className="text-lg font-semibold text-foreground">
            All Appointments
          </CardTitle>
          <div className="flex items-center gap-2">
            <div className="relative">
              <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
              <Input
                placeholder="Search..."
                value={search}
                onChange={(e) => setSearch(e.target.value)}
                className="w-48 border-border bg-card pl-9"
              />
            </div>
            <button className="rounded-lg border border-border bg-card p-2 text-muted-foreground transition-colors hover:bg-secondary hover:text-foreground">
              <Filter className="h-4 w-4" />
            </button>
          </div>
        </CardHeader>
        <CardContent>
          {/* Table header */}
          <div className="hidden grid-cols-6 gap-4 rounded-lg bg-secondary px-4 py-3 text-xs font-semibold uppercase text-muted-foreground md:grid">
            <span>Patient</span>
            <span>Type</span>
            <span>Date</span>
            <span>Time</span>
            <span>Status</span>
            <span>Action</span>
          </div>
          <div className="flex flex-col gap-2 md:gap-0">
            {filteredAppointments.map((apt) => (
              <div
                key={apt.id}
                className="grid grid-cols-1 gap-2 rounded-lg border border-border p-4 md:grid-cols-6 md:items-center md:gap-4 md:rounded-none md:border-0 md:border-b md:p-4"
              >
                <div className="flex items-center gap-3">
                  <Avatar className="h-9 w-9">
                    <AvatarImage src={apt.avatar || "/placeholder.svg"} alt={apt.patient} />
                    <AvatarFallback>{apt.patient[0]}</AvatarFallback>
                  </Avatar>
                  <div>
                    <p className="text-sm font-medium text-foreground">{apt.patient}</p>
                    <p className="text-xs text-muted-foreground">{apt.age}</p>
                  </div>
                </div>
                <p className="text-sm text-muted-foreground">{apt.type}</p>
                <p className="text-sm text-muted-foreground">{apt.date}</p>
                <p className="text-sm text-muted-foreground">{apt.time}</p>
                <div>
                  <Badge variant="secondary" className={cn("border-0", getStatusClasses(apt.status))}>
                    {apt.status}
                  </Badge>
                </div>
                <div className="flex gap-2">
                  <button className="rounded-lg bg-primary px-3 py-1.5 text-xs font-medium text-primary-foreground transition-opacity hover:opacity-90">
                    Accept
                  </button>
                  <button className="rounded-lg border border-border bg-card px-3 py-1.5 text-xs font-medium text-muted-foreground transition-colors hover:bg-secondary hover:text-foreground">
                    Cancel
                  </button>
                </div>
              </div>
            ))}
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
