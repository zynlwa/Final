"use client"

import { useState } from "react"
import Link from "next/link"
import {
  User,
  Mail,
  Phone,
  MapPin,
  Droplets,
  Heart,
  Stethoscope,
  Sparkles,
  FileText,
  Globe,
  GraduationCap,
  Clock,
  Star,
  CalendarDays,
  Users,
} from "lucide-react"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Progress } from "@/components/ui/progress"
import { cn } from "@/lib/utils"
import { useAuth } from "@/context/auth-context"

/* ─── Patient data ─── */
const patientInfo = {
  name: "Christopher Burrell",
  age: "25 Years old",
  gender: "Female",
  birthday: "13th Sep 1993",
  phone: "+(125) 458-8547",
  address: "Sydney, Australia",
  bloodGroup: "B +",
  profileCompletion: 89,
  avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=200&h=200&fit=crop&crop=face",
  introduction: "Web designers to occupy the space which will later be filled with real content. This is required when, for example, the final text is not yet available. Dummy text is also known as fill text. Dummy texts have been in use by typesetters since the 16th century.",
}

const patientAppointments = [
  { type: "Cardiogram", doctor: "Dr. Calvin Carlo", date: "13 March", icon: Heart, color: "text-rose-500" },
  { type: "Checkup", doctor: "Dr. Cristino Murphy", date: "5 May", icon: Stethoscope, color: "text-emerald-500" },
  { type: "Covid Test", doctor: "Dr. Alia Reddy", date: "19 June", icon: Sparkles, color: "text-amber-500" },
  { type: "Dentist", doctor: "Dr. James Moore", date: "20 July", icon: Stethoscope, color: "text-sky-500" },
]

const patientPayments = [
  { type: "Cardiogram", status: "Full bill paid" },
  { type: "Covid Test", status: "Full bill paid" },
  { type: "Checkup", status: "Full bill paid" },
  { type: "Dentist", status: "Full bill paid" },
]

const patientInfoItems = [
  { icon: User, label: "Gender", value: patientInfo.gender },
  { icon: Mail, label: "Birthday", value: patientInfo.birthday },
  { icon: Phone, label: "Phone No.", value: patientInfo.phone },
  { icon: MapPin, label: "Address", value: patientInfo.address },
  { icon: Droplets, label: "Blood Group", value: patientInfo.bloodGroup },
]

/* ─── Doctor data ─── */
const doctorInfo = {
  name: "Dr. Calvin Carlo",
  specialty: "Orthopedic",
  regNo: "A-36589",
  phone: "+1 (700) 230-0035",
  email: "calvin@example.com",
  address: "California, United States",
  profileCompletion: 95,
  avatar: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=200&h=200&fit=crop&crop=face",
  introduction: "Dr. Calvin Carlo is a highly skilled orthopedic surgeon with over 15 years of experience. He specializes in joint replacement, sports medicine, and spine surgery. His commitment to patient care and use of cutting-edge techniques have earned him recognition among peers.",
  education: "MBBS (University of Wyoming), M.D. of Medicine (Netherland Medical College), Senior Prof. (MBBS, M.D) Netherland Medical College.",
  experience: "Over 15 years of experience in orthopedic surgery with specializations in minimally invasive procedures, sports injury rehabilitation, and bone reconstruction.",
}

const doctorInfoItems = [
  { icon: GraduationCap, label: "Specialty", value: doctorInfo.specialty },
  { icon: FileText, label: "Reg No.", value: doctorInfo.regNo },
  { icon: Phone, label: "Phone", value: doctorInfo.phone },
  { icon: Mail, label: "Email", value: doctorInfo.email },
  { icon: MapPin, label: "Address", value: doctorInfo.address },
]

const doctorStats = [
  { label: "Patients", value: "180+", icon: Users },
  { label: "Experience", value: "15+ yrs", icon: Clock },
  { label: "Rating", value: "4.8", icon: Star },
  { label: "Appointments", value: "1200+", icon: CalendarDays },
]

/* ─── Patient Profile ─── */
function PatientProfile() {
  const [activeTab, setActiveTab] = useState<"profile" | "settings">("profile")

  return (
    <div className="flex flex-col gap-6 lg:flex-row">
      <div className="w-full lg:w-80 lg:shrink-0">
        <Card className="border-border shadow-sm">
          <CardContent className="p-0">
            <div className="relative h-28 w-full overflow-hidden rounded-t-xl">
              <svg viewBox="0 0 400 120" className="h-full w-full" preserveAspectRatio="none">
                <path d="M0 120 C50 80, 100 100, 150 60 C200 20, 250 80, 300 40 C350 0, 400 50, 400 0 L400 120 Z" fill="hsl(270, 60%, 50%)" />
                <path d="M0 120 C80 70, 120 90, 200 50 C280 10, 350 70, 400 30 L400 120 Z" fill="hsl(150, 60%, 50%)" opacity="0.7" />
                <path d="M0 120 C60 90, 150 50, 250 80 C350 110, 380 60, 400 80 L400 120 Z" fill="hsl(45, 80%, 60%)" opacity="0.6" />
              </svg>
              <div className="absolute -bottom-10 left-1/2 -translate-x-1/2">
                <Avatar className="h-20 w-20 border-4 border-card">
                  <AvatarImage src={patientInfo.avatar || "/placeholder.svg"} alt={patientInfo.name} />
                  <AvatarFallback>CB</AvatarFallback>
                </Avatar>
              </div>
            </div>
            <div className="mt-8 px-5 pb-5 pt-4 text-center">
              <h2 className="text-lg font-semibold text-foreground">{patientInfo.name}</h2>
              <p className="text-sm text-muted-foreground">{patientInfo.age}</p>
            </div>
            <div className="border-t border-border px-5 py-4">
              <div className="mb-2 flex items-center justify-between">
                <span className="text-sm font-medium text-foreground">Complete your profile</span>
                <span className="text-sm font-semibold text-primary">{patientInfo.profileCompletion}%</span>
              </div>
              <Progress value={patientInfo.profileCompletion} className="h-2" />
            </div>
            <div className="border-t border-border px-5 py-4">
              <ul className="flex flex-col gap-4">
                {patientInfoItems.map((item) => (
                  <li key={item.label} className="flex items-center gap-3">
                    <item.icon className="h-4 w-4 text-muted-foreground" />
                    <span className="text-sm font-medium text-foreground">{item.label}</span>
                    <span className="ml-auto text-sm text-muted-foreground">{item.value}</span>
                  </li>
                ))}
              </ul>
            </div>
          </CardContent>
        </Card>
      </div>

      <div className="flex-1">
        <div className="mb-6 flex overflow-hidden rounded-xl border border-border bg-card">
          <button onClick={() => setActiveTab("profile")} className={cn("flex-1 px-6 py-3 text-sm font-semibold transition-colors", activeTab === "profile" ? "bg-primary text-primary-foreground" : "text-muted-foreground hover:text-foreground")}>
            Profile
          </button>
          <button onClick={() => setActiveTab("settings")} className={cn("flex-1 px-6 py-3 text-sm font-semibold transition-colors", activeTab === "settings" ? "bg-primary text-primary-foreground" : "text-muted-foreground hover:text-foreground")}>
            Profile Settings
          </button>
        </div>

        {activeTab === "profile" && (
          <div className="flex flex-col gap-6">
            <Card className="border-border shadow-sm">
              <CardHeader className="pb-2">
                <CardTitle className="text-lg font-semibold text-foreground">Introduction:</CardTitle>
              </CardHeader>
              <CardContent>
                <p className="leading-relaxed text-muted-foreground">{patientInfo.introduction}</p>
              </CardContent>
            </Card>

            <div className="grid grid-cols-1 gap-6 lg:grid-cols-2">
              <Card className="border-border shadow-sm">
                <CardHeader className="pb-3">
                  <CardTitle className="text-lg font-semibold text-foreground">Appointment List</CardTitle>
                </CardHeader>
                <CardContent className="flex flex-col gap-3">
                  {patientAppointments.map((apt) => (
                    <div key={apt.type} className="flex items-center gap-4 rounded-lg border border-border p-3">
                      <div className="flex h-10 w-10 items-center justify-center rounded-full bg-secondary">
                        <apt.icon className={cn("h-5 w-5", apt.color)} />
                      </div>
                      <div className="flex-1">
                        <p className="text-sm font-semibold text-foreground">{apt.type}</p>
                        <p className="text-xs text-muted-foreground">{apt.doctor}</p>
                      </div>
                      <span className="text-sm text-muted-foreground">{apt.date}</span>
                    </div>
                  ))}
                </CardContent>
              </Card>

              <Card className="border-border shadow-sm">
                <CardHeader className="pb-3">
                  <CardTitle className="text-lg font-semibold text-foreground">Payment List</CardTitle>
                </CardHeader>
                <CardContent className="flex flex-col gap-3">
                  {patientPayments.map((p) => (
                    <div key={p.type} className="flex items-center gap-4 rounded-lg border border-border p-3">
                      <div className="flex-1">
                        <p className="text-sm font-semibold text-foreground">{p.type}</p>
                        <p className="text-xs text-muted-foreground">{p.status}</p>
                      </div>
                      <button className="flex h-10 w-10 items-center justify-center rounded-lg bg-primary text-primary-foreground transition-opacity hover:opacity-90" aria-label="Download invoice">
                        <FileText className="h-5 w-5" />
                      </button>
                    </div>
                  ))}
                </CardContent>
              </Card>
            </div>
          </div>
        )}

        {activeTab === "settings" && (
          <Card className="border-border shadow-sm">
            <CardContent className="p-6">
              <p className="text-muted-foreground">
                Go to{" "}
                <Link href="/dashboard/profile-settings" className="font-medium text-primary hover:underline">
                  Profile Settings
                </Link>{" "}
                page for editing your information.
              </p>
            </CardContent>
          </Card>
        )}
      </div>
    </div>
  )
}

/* ─── Doctor Profile ─── */
function DoctorProfile() {
  const [activeTab, setActiveTab] = useState<"profile" | "settings">("profile")

  return (
    <div className="flex flex-col gap-6 lg:flex-row">
      <div className="w-full lg:w-80 lg:shrink-0">
        <Card className="border-border shadow-sm">
          <CardContent className="p-0">
            <div className="relative h-28 w-full overflow-hidden rounded-t-xl bg-gradient-to-r from-[hsl(234,85%,30%)] via-[hsl(234,85%,45%)] to-[hsl(234,85%,60%)]">
              <svg viewBox="0 0 300 80" className="h-full w-full opacity-40">
                <polyline fill="none" stroke="hsl(180, 100%, 70%)" strokeWidth="2" points="0,50 30,50 40,20 50,60 60,35 70,50 100,50 130,50 140,15 150,65 160,30 170,50 200,50 230,50 240,20 250,60 260,35 270,50 300,50" />
              </svg>
              <div className="absolute -bottom-10 left-1/2 -translate-x-1/2">
                <Avatar className="h-20 w-20 border-4 border-card">
                  <AvatarImage src={doctorInfo.avatar || "/placeholder.svg"} alt={doctorInfo.name} />
                  <AvatarFallback>DC</AvatarFallback>
                </Avatar>
              </div>
            </div>
            <div className="mt-8 px-5 pb-5 pt-4 text-center">
              <h2 className="text-lg font-semibold text-foreground">{doctorInfo.name}</h2>
              <p className="text-sm text-primary">{doctorInfo.specialty}</p>
            </div>
            <div className="border-t border-border px-5 py-4">
              <div className="mb-2 flex items-center justify-between">
                <span className="text-sm font-medium text-foreground">Profile completion</span>
                <span className="text-sm font-semibold text-primary">{doctorInfo.profileCompletion}%</span>
              </div>
              <Progress value={doctorInfo.profileCompletion} className="h-2" />
            </div>
            <div className="border-t border-border px-5 py-4">
              <ul className="flex flex-col gap-4">
                {doctorInfoItems.map((item) => (
                  <li key={item.label} className="flex items-center gap-3">
                    <item.icon className="h-4 w-4 text-muted-foreground" />
                    <span className="text-sm font-medium text-foreground">{item.label}</span>
                    <span className="ml-auto text-sm text-muted-foreground">{item.value}</span>
                  </li>
                ))}
              </ul>
            </div>
            <div className="border-t border-border px-5 py-4">
              <div className="grid grid-cols-2 gap-3">
                {doctorStats.map((s) => (
                  <div key={s.label} className="flex flex-col items-center rounded-lg bg-secondary p-3">
                    <s.icon className="mb-1 h-5 w-5 text-primary" />
                    <span className="text-sm font-bold text-foreground">{s.value}</span>
                    <span className="text-xs text-muted-foreground">{s.label}</span>
                  </div>
                ))}
              </div>
            </div>
          </CardContent>
        </Card>
      </div>

      <div className="flex-1">
        <div className="mb-6 flex overflow-hidden rounded-xl border border-border bg-card">
          <button onClick={() => setActiveTab("profile")} className={cn("flex-1 px-6 py-3 text-sm font-semibold transition-colors", activeTab === "profile" ? "bg-primary text-primary-foreground" : "text-muted-foreground hover:text-foreground")}>
            Profile
          </button>
          <button onClick={() => setActiveTab("settings")} className={cn("flex-1 px-6 py-3 text-sm font-semibold transition-colors", activeTab === "settings" ? "bg-primary text-primary-foreground" : "text-muted-foreground hover:text-foreground")}>
            Profile Settings
          </button>
        </div>

        {activeTab === "profile" && (
          <div className="flex flex-col gap-6">
            <Card className="border-border shadow-sm">
              <CardHeader className="pb-2">
                <CardTitle className="text-lg font-semibold text-foreground">About Me</CardTitle>
              </CardHeader>
              <CardContent>
                <p className="leading-relaxed text-muted-foreground">{doctorInfo.introduction}</p>
              </CardContent>
            </Card>

            <Card className="border-border shadow-sm">
              <CardHeader className="pb-2">
                <CardTitle className="text-lg font-semibold text-foreground">Educational Background</CardTitle>
              </CardHeader>
              <CardContent>
                <p className="leading-relaxed text-muted-foreground">{doctorInfo.education}</p>
              </CardContent>
            </Card>

            <Card className="border-border shadow-sm">
              <CardHeader className="pb-2">
                <CardTitle className="text-lg font-semibold text-foreground">Medical Experience & Skills</CardTitle>
              </CardHeader>
              <CardContent>
                <p className="leading-relaxed text-muted-foreground">{doctorInfo.experience}</p>
              </CardContent>
            </Card>

            <Card className="border-border shadow-sm">
              <CardHeader className="pb-3">
                <CardTitle className="text-lg font-semibold text-foreground">Working Hours</CardTitle>
              </CardHeader>
              <CardContent>
                <div className="flex flex-col gap-2">
                  {[
                    { day: "Monday - Friday", hours: "8:00 AM - 6:00 PM" },
                    { day: "Saturday", hours: "9:00 AM - 2:00 PM" },
                    { day: "Sunday", hours: "Closed" },
                  ].map((wh) => (
                    <div key={wh.day} className="flex items-center justify-between rounded-lg border border-border p-3">
                      <span className="text-sm font-medium text-foreground">{wh.day}</span>
                      <span className={cn("text-sm", wh.hours === "Closed" ? "font-medium text-destructive" : "text-muted-foreground")}>{wh.hours}</span>
                    </div>
                  ))}
                </div>
              </CardContent>
            </Card>
          </div>
        )}

        {activeTab === "settings" && (
          <Card className="border-border shadow-sm">
            <CardContent className="p-6">
              <p className="text-muted-foreground">
                Go to{" "}
                <Link href="/dashboard/profile-settings" className="font-medium text-primary hover:underline">
                  Profile Settings
                </Link>{" "}
                page for editing your information.
              </p>
            </CardContent>
          </Card>
        )}
      </div>
    </div>
  )
}

export default function ProfilePage() {
  const { role } = useAuth()

  if (role === "patient") {
    return <PatientProfile />
  }
  return <DoctorProfile />
}
