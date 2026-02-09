"use client"

import {
  CalendarDays,
  Users,
  DollarSign,
  Activity,
  ArrowUpRight,
  ArrowDownRight,
  Clock,
  Heart,
  Stethoscope,
  FileText,
  Sparkles,
} from "lucide-react"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { useAuth } from "@/context/auth-context"
import Link from "next/link"

/* ─── Doctor data ─── */
const doctorStats = [
  { title: "Appointments", value: "24", change: "+12%", trend: "up", icon: CalendarDays },
  { title: "Total Patients", value: "180", change: "+8%", trend: "up", icon: Users },
  { title: "Revenue", value: "$12,450", change: "+15%", trend: "up", icon: DollarSign },
  { title: "Avg. Rating", value: "4.8", change: "-0.1", trend: "down", icon: Activity },
]

const recentAppointments = [
  { name: "Christopher Burrell", age: "25 Years", date: "13 March", type: "Cardiogram", status: "Approved", avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=80&h=80&fit=crop&crop=face" },
  { name: "Sarah Johnson", age: "32 Years", date: "14 March", type: "Checkup", status: "Pending", avatar: "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=80&h=80&fit=crop&crop=face" },
  { name: "Michael Smith", age: "45 Years", date: "15 March", type: "Covid Test", status: "Approved", avatar: "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=80&h=80&fit=crop&crop=face" },
  { name: "Emily Davis", age: "28 Years", date: "16 March", type: "Dentist", status: "Canceled", avatar: "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=80&h=80&fit=crop&crop=face" },
]

const upcomingSlots = [
  { time: "09:00 AM", patient: "Christopher Burrell", type: "Cardiogram" },
  { time: "10:30 AM", patient: "Sarah Johnson", type: "Checkup" },
  { time: "01:00 PM", patient: "Michael Smith", type: "Covid Test" },
  { time: "03:30 PM", patient: "Emily Davis", type: "Consultation" },
]

/* ─── Patient data ─── */
const patientStats = [
  { title: "Appointments", value: "6", change: "+2", trend: "up", icon: CalendarDays },
  { title: "Prescriptions", value: "12", change: "+1", trend: "up", icon: FileText },
  { title: "Total Spent", value: "$2,340", change: "+$180", trend: "up", icon: DollarSign },
  { title: "Pending Reports", value: "2", change: "-1", trend: "down", icon: Activity },
]

const patientAppointments = [
  { type: "Cardiogram", doctor: "Dr. Calvin Carlo", date: "13 March", icon: Heart, color: "text-rose-500", status: "Completed" },
  { type: "Checkup", doctor: "Dr. Cristino Murphy", date: "5 May", icon: Stethoscope, color: "text-emerald-500", status: "Upcoming" },
  { type: "Covid Test", doctor: "Dr. Alia Reddy", date: "19 June", icon: Sparkles, color: "text-amber-500", status: "Upcoming" },
  { type: "Dentist", doctor: "Dr. James Moore", date: "20 July", icon: Stethoscope, color: "text-sky-500", status: "Upcoming" },
]

function getStatusColor(status: string) {
  switch (status) {
    case "Approved": case "Completed":
      return "bg-emerald-100 text-emerald-700"
    case "Pending": case "Upcoming":
      return "bg-amber-100 text-amber-700"
    case "Canceled":
      return "bg-red-100 text-red-700"
    default:
      return "bg-secondary text-secondary-foreground"
  }
}

function DoctorDashboard() {
  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Dashboard</h1>
        <p className="text-sm text-muted-foreground">Welcome back, Dr. Calvin Carlo</p>
      </div>

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 xl:grid-cols-4">
        {doctorStats.map((stat) => (
          <Card key={stat.title} className="border-border shadow-sm">
            <CardContent className="flex items-center gap-4 p-5">
              <div className="flex h-12 w-12 shrink-0 items-center justify-center rounded-xl bg-primary/10">
                <stat.icon className="h-6 w-6 text-primary" />
              </div>
              <div className="flex-1">
                <p className="text-sm text-muted-foreground">{stat.title}</p>
                <div className="flex items-center gap-2">
                  <p className="text-xl font-bold text-foreground">{stat.value}</p>
                  <span className={`flex items-center text-xs font-medium ${stat.trend === "up" ? "text-emerald-600" : "text-red-500"}`}>
                    {stat.trend === "up" ? <ArrowUpRight className="h-3 w-3" /> : <ArrowDownRight className="h-3 w-3" />}
                    {stat.change}
                  </span>
                </div>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>

      <div className="grid grid-cols-1 gap-6 xl:grid-cols-3">
        <Card className="border-border shadow-sm xl:col-span-2">
          <CardHeader className="pb-3">
            <CardTitle className="text-lg font-semibold text-foreground">Recent Appointments</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="flex flex-col gap-3">
              {recentAppointments.map((apt) => (
                <div key={apt.name} className="flex items-center gap-4 rounded-lg border border-border p-3 transition-colors hover:bg-secondary/50">
                  <Avatar className="h-10 w-10">
                    <AvatarImage src={apt.avatar || "/placeholder.svg"} alt={apt.name} />
                    <AvatarFallback>{apt.name[0]}</AvatarFallback>
                  </Avatar>
                  <div className="flex-1">
                    <p className="text-sm font-medium text-foreground">{apt.name}</p>
                    <p className="text-xs text-muted-foreground">{apt.type} - {apt.age}</p>
                  </div>
                  <div className="hidden text-right sm:block">
                    <p className="text-sm text-muted-foreground">{apt.date}</p>
                  </div>
                  <Badge variant="secondary" className={`${getStatusColor(apt.status)} border-0`}>{apt.status}</Badge>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>

        <Card className="border-border shadow-sm">
          <CardHeader className="pb-3">
            <CardTitle className="text-lg font-semibold text-foreground">Upcoming Slots</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="flex flex-col gap-3">
              {upcomingSlots.map((slot) => (
                <div key={slot.time} className="flex items-center gap-3 rounded-lg border border-border p-3">
                  <div className="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg bg-primary/10">
                    <Clock className="h-5 w-5 text-primary" />
                  </div>
                  <div className="flex-1">
                    <p className="text-sm font-medium text-foreground">{slot.patient}</p>
                    <p className="text-xs text-muted-foreground">{slot.type}</p>
                  </div>
                  <span className="text-xs font-medium text-primary">{slot.time}</span>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}

function PatientDashboard() {
  const { user } = useAuth()

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Dashboard</h1>
        <p className="text-sm text-muted-foreground">Welcome back, {user?.name ?? "Patient"}</p>
      </div>

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 xl:grid-cols-4">
        {patientStats.map((stat) => (
          <Card key={stat.title} className="border-border shadow-sm">
            <CardContent className="flex items-center gap-4 p-5">
              <div className="flex h-12 w-12 shrink-0 items-center justify-center rounded-xl bg-primary/10">
                <stat.icon className="h-6 w-6 text-primary" />
              </div>
              <div className="flex-1">
                <p className="text-sm text-muted-foreground">{stat.title}</p>
                <div className="flex items-center gap-2">
                  <p className="text-xl font-bold text-foreground">{stat.value}</p>
                  <span className={`flex items-center text-xs font-medium ${stat.trend === "up" ? "text-emerald-600" : "text-red-500"}`}>
                    {stat.trend === "up" ? <ArrowUpRight className="h-3 w-3" /> : <ArrowDownRight className="h-3 w-3" />}
                    {stat.change}
                  </span>
                </div>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>

      <div className="grid grid-cols-1 gap-6 xl:grid-cols-3">
        <Card className="border-border shadow-sm xl:col-span-2">
          <CardHeader className="flex flex-row items-center justify-between pb-3">
            <CardTitle className="text-lg font-semibold text-foreground">My Appointments</CardTitle>
            <Link href="/book-appointment" className="rounded-lg bg-primary px-4 py-2 text-xs font-medium text-primary-foreground transition-opacity hover:opacity-90">
              Book New
            </Link>
          </CardHeader>
          <CardContent>
            <div className="flex flex-col gap-3">
              {patientAppointments.map((apt) => (
                <div key={apt.type + apt.date} className="flex items-center gap-4 rounded-lg border border-border p-3 transition-colors hover:bg-secondary/50">
                  <div className="flex h-10 w-10 items-center justify-center rounded-full bg-secondary">
                    <apt.icon className={`h-5 w-5 ${apt.color}`} />
                  </div>
                  <div className="flex-1">
                    <p className="text-sm font-semibold text-foreground">{apt.type}</p>
                    <p className="text-xs text-muted-foreground">{apt.doctor}</p>
                  </div>
                  <div className="flex items-center gap-3">
                    <span className="hidden text-sm text-muted-foreground sm:block">{apt.date}</span>
                    <Badge variant="secondary" className={`${getStatusColor(apt.status)} border-0`}>{apt.status}</Badge>
                  </div>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>

        <Card className="border-border shadow-sm">
          <CardHeader className="pb-3">
            <CardTitle className="text-lg font-semibold text-foreground">Quick Actions</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="flex flex-col gap-3">
              <Link href="/book-appointment" className="flex items-center gap-3 rounded-lg border border-border p-3 transition-colors hover:bg-secondary/50">
                <div className="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg bg-primary/10">
                  <Stethoscope className="h-5 w-5 text-primary" />
                </div>
                <div className="flex-1">
                  <p className="text-sm font-medium text-foreground">Book Appointment</p>
                  <p className="text-xs text-muted-foreground">Schedule a new visit</p>
                </div>
              </Link>
              <Link href="/dashboard/prescriptions" className="flex items-center gap-3 rounded-lg border border-border p-3 transition-colors hover:bg-secondary/50">
                <div className="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg bg-primary/10">
                  <FileText className="h-5 w-5 text-primary" />
                </div>
                <div className="flex-1">
                  <p className="text-sm font-medium text-foreground">Prescriptions</p>
                  <p className="text-xs text-muted-foreground">View active prescriptions</p>
                </div>
              </Link>
              <Link href="/dashboard/medical-records" className="flex items-center gap-3 rounded-lg border border-border p-3 transition-colors hover:bg-secondary/50">
                <div className="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg bg-primary/10">
                  <Activity className="h-5 w-5 text-primary" />
                </div>
                <div className="flex-1">
                  <p className="text-sm font-medium text-foreground">Medical Records</p>
                  <p className="text-xs text-muted-foreground">Access your health records</p>
                </div>
              </Link>
              <Link href="/doctors" className="flex items-center gap-3 rounded-lg border border-border p-3 transition-colors hover:bg-secondary/50">
                <div className="flex h-10 w-10 shrink-0 items-center justify-center rounded-lg bg-primary/10">
                  <Heart className="h-5 w-5 text-primary" />
                </div>
                <div className="flex-1">
                  <p className="text-sm font-medium text-foreground">Find Doctors</p>
                  <p className="text-xs text-muted-foreground">Browse doctor directory</p>
                </div>
              </Link>
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}

export default function DashboardPage() {
  const { role } = useAuth()

  if (role === "patient") {
    return <PatientDashboard />
  }
  return <DoctorDashboard />
}
