"use client"

import Link from "next/link"
import { Heart, Stethoscope, Sparkles } from "lucide-react"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { cn } from "@/lib/utils"

const appointments = [
  { type: "Cardiogram", doctor: "Dr. Calvin Carlo", date: "13 March 2026", time: "09:00 AM", icon: Heart, color: "text-rose-500", status: "Completed" },
  { type: "Checkup", doctor: "Dr. Cristino Murphy", date: "5 May 2026", time: "10:30 AM", icon: Stethoscope, color: "text-emerald-500", status: "Upcoming" },
  { type: "Covid Test", doctor: "Dr. Alia Reddy", date: "19 June 2026", time: "02:00 PM", icon: Sparkles, color: "text-amber-500", status: "Upcoming" },
  { type: "Dentist", doctor: "Dr. James Moore", date: "20 July 2026", time: "11:00 AM", icon: Stethoscope, color: "text-sky-500", status: "Upcoming" },
  { type: "Orthopedic", doctor: "Dr. Toni Kovar", date: "15 Aug 2026", time: "03:30 PM", icon: Heart, color: "text-purple-500", status: "Pending" },
]

function getStatusColor(status: string) {
  switch (status) {
    case "Completed": return "bg-emerald-100 text-emerald-700"
    case "Upcoming": return "bg-amber-100 text-amber-700"
    case "Pending": return "bg-sky-100 text-sky-700"
    case "Canceled": return "bg-red-100 text-red-700"
    default: return "bg-secondary text-secondary-foreground"
  }
}

export default function MyAppointmentsPage() {
  return (
    <div className="flex flex-col gap-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-foreground">My Appointments</h1>
          <p className="text-sm text-muted-foreground">View and manage your upcoming appointments</p>
        </div>
        <Link href="/book-appointment" className="rounded-lg bg-primary px-5 py-2.5 text-sm font-medium text-primary-foreground transition-opacity hover:opacity-90">
          Book New
        </Link>
      </div>

      <Card className="border-border shadow-sm">
        <CardHeader className="pb-3">
          <CardTitle className="text-lg font-semibold text-foreground">All Appointments</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="flex flex-col gap-3">
            {appointments.map((apt) => (
              <div key={apt.type + apt.date} className="flex items-center gap-4 rounded-lg border border-border p-4 transition-colors hover:bg-secondary/50">
                <div className="flex h-12 w-12 shrink-0 items-center justify-center rounded-full bg-secondary">
                  <apt.icon className={cn("h-6 w-6", apt.color)} />
                </div>
                <div className="flex-1">
                  <p className="text-sm font-semibold text-foreground">{apt.type}</p>
                  <p className="text-xs text-muted-foreground">{apt.doctor}</p>
                </div>
                <div className="hidden flex-col items-end gap-1 sm:flex">
                  <p className="text-sm text-foreground">{apt.date}</p>
                  <p className="text-xs text-muted-foreground">{apt.time}</p>
                </div>
                <Badge variant="secondary" className={cn("border-0", getStatusColor(apt.status))}>{apt.status}</Badge>
              </div>
            ))}
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
