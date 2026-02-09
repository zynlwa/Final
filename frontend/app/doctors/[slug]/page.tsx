"use client"

import { useState } from "react"
import Link from "next/link"
import { useParams } from "next/navigation"
import {
  Star,
  MapPin,
  Phone,
  Mail,
  Clock,
  Award,
  GraduationCap,
  Briefcase,
  Facebook,
  Twitter,
  Linkedin,
  Instagram,
} from "lucide-react"
import Navbar from "@/components/navbar"
import Footer from "@/components/footer"

const doctorsData: Record<string, {
  name: string
  specialty: string
  qualification: string
  location: string
  rating: number
  reviews: number
  avatar: string
  regNo: string
  phone: string
  email: string
  bio: string
  education: string
  experience: string
  workingHours: { day: string; time: string }[]
}> = {
  "dr-calvin-carlo": {
    name: "Dr. Calvin Carlo",
    specialty: "Orthopedic",
    qualification: "M.B.B.S, M.D",
    location: "New York, USA",
    rating: 4.9,
    reviews: 128,
    avatar: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=400&h=400&fit=crop&crop=face",
    regNo: "A-36589",
    phone: "+1 (700) 230-0035",
    email: "calvin@medicare.com",
    bio: "Dr. Calvin Carlo is a renowned orthopedic surgeon with over 15 years of experience. He specializes in joint replacement surgery, sports medicine, and trauma care. His dedication to patient care and innovative surgical techniques have earned him recognition across the medical community.",
    education: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    experience: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    workingHours: [
      { day: "Monday", time: "9:00 AM - 5:00 PM" },
      { day: "Tuesday", time: "9:00 AM - 5:00 PM" },
      { day: "Wednesday", time: "9:00 AM - 5:00 PM" },
      { day: "Thursday", time: "9:00 AM - 5:00 PM" },
      { day: "Friday", time: "9:00 AM - 3:00 PM" },
      { day: "Saturday", time: "10:00 AM - 1:00 PM" },
    ],
  },
  "dr-cristino-murphy": {
    name: "Dr. Cristino Murphy",
    specialty: "Gynecologist",
    qualification: "M.B.B.S",
    location: "London, UK",
    rating: 4.8,
    reviews: 95,
    avatar: "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=400&h=400&fit=crop&crop=face",
    regNo: "B-25478",
    phone: "+44 20 7946 0958",
    email: "cristino@medicare.com",
    bio: "Dr. Cristino Murphy is a highly skilled gynecologist with expertise in women's health, prenatal care, and minimally invasive surgery. With 12 years of dedicated practice, she has helped thousands of patients with compassionate and evidence-based care.",
    education: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation.",
    experience: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua. Ut enim ad minim veniam.",
    workingHours: [
      { day: "Monday", time: "10:00 AM - 6:00 PM" },
      { day: "Tuesday", time: "10:00 AM - 6:00 PM" },
      { day: "Wednesday", time: "10:00 AM - 6:00 PM" },
      { day: "Thursday", time: "10:00 AM - 6:00 PM" },
      { day: "Friday", time: "10:00 AM - 4:00 PM" },
    ],
  },
  "dr-alia-reddy": {
    name: "Dr. Alia Reddy",
    specialty: "Psychotherapist",
    qualification: "M.B.B.S",
    location: "Sydney, Australia",
    rating: 4.7,
    reviews: 82,
    avatar: "https://images.unsplash.com/photo-1594824476967-48c8b964ac31?w=400&h=400&fit=crop&crop=face",
    regNo: "C-78965",
    phone: "+61 2 9876 5432",
    email: "alia@medicare.com",
    bio: "Dr. Alia Reddy is a compassionate psychotherapist specializing in cognitive behavioral therapy, anxiety management, and stress-related disorders. She provides holistic mental health care to help patients lead fulfilling lives.",
    education: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua.",
    experience: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua.",
    workingHours: [
      { day: "Monday", time: "9:00 AM - 4:00 PM" },
      { day: "Tuesday", time: "9:00 AM - 4:00 PM" },
      { day: "Wednesday", time: "9:00 AM - 4:00 PM" },
      { day: "Thursday", time: "9:00 AM - 4:00 PM" },
      { day: "Friday", time: "9:00 AM - 2:00 PM" },
    ],
  },
  "dr-james-moore": {
    name: "Dr. James Moore",
    specialty: "Dentistry",
    qualification: "M.B.B.S, D.D.S",
    location: "Toronto, Canada",
    rating: 4.9,
    reviews: 156,
    avatar: "https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=400&h=400&fit=crop&crop=face",
    regNo: "D-45123",
    phone: "+1 (416) 555-0198",
    email: "james@medicare.com",
    bio: "Dr. James Moore is a skilled dentist specializing in cosmetic dentistry, implants, and oral surgery. With a focus on patient comfort and cutting-edge techniques, he delivers exceptional dental care.",
    education: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua.",
    experience: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua.",
    workingHours: [
      { day: "Monday", time: "8:00 AM - 5:00 PM" },
      { day: "Tuesday", time: "8:00 AM - 5:00 PM" },
      { day: "Wednesday", time: "8:00 AM - 5:00 PM" },
      { day: "Thursday", time: "8:00 AM - 5:00 PM" },
      { day: "Friday", time: "8:00 AM - 3:00 PM" },
    ],
  },
  "dr-sarah-williams": {
    name: "Dr. Sarah Williams",
    specialty: "Dermatology",
    qualification: "M.B.B.S, M.D",
    location: "Berlin, Germany",
    rating: 4.6,
    reviews: 67,
    avatar: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=400&h=400&fit=crop&crop=face",
    regNo: "E-98745",
    phone: "+49 30 1234 5678",
    email: "sarah@medicare.com",
    bio: "Dr. Sarah Williams is an expert dermatologist specializing in skin care, acne treatment, and cosmetic procedures. She brings a personalized approach to each patient's unique skin concerns.",
    education: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua.",
    experience: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua.",
    workingHours: [
      { day: "Monday", time: "9:00 AM - 5:00 PM" },
      { day: "Tuesday", time: "9:00 AM - 5:00 PM" },
      { day: "Wednesday", time: "9:00 AM - 5:00 PM" },
      { day: "Thursday", time: "9:00 AM - 5:00 PM" },
      { day: "Friday", time: "9:00 AM - 2:00 PM" },
    ],
  },
  "dr-michael-chen": {
    name: "Dr. Michael Chen",
    specialty: "Ophthalmology",
    qualification: "M.B.B.S, M.S",
    location: "Tokyo, Japan",
    rating: 4.8,
    reviews: 110,
    avatar: "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=400&h=400&fit=crop&crop=face",
    regNo: "F-65432",
    phone: "+81 3 1234 5678",
    email: "michael@medicare.com",
    bio: "Dr. Michael Chen is an experienced ophthalmologist specializing in cataract surgery, LASIK, and retinal diseases. His precision and care have restored vision for hundreds of patients.",
    education: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua.",
    experience: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor magna aliqua.",
    workingHours: [
      { day: "Monday", time: "9:00 AM - 6:00 PM" },
      { day: "Tuesday", time: "9:00 AM - 6:00 PM" },
      { day: "Wednesday", time: "9:00 AM - 6:00 PM" },
      { day: "Thursday", time: "9:00 AM - 6:00 PM" },
      { day: "Friday", time: "9:00 AM - 3:00 PM" },
    ],
  },
}

const timeSlots = ["09:00 AM", "09:30 AM", "10:00 AM", "10:30 AM", "11:00 AM", "02:00 PM", "02:30 PM", "03:00 PM"]

export default function DoctorDetailPage() {
  const params = useParams()
  const slug = params.slug as string
  const doctor = doctorsData[slug]
  const [appointmentData, setAppointmentData] = useState({ name: "", email: "", date: "", time: "" })

  if (!doctor) {
    return (
      <div className="min-h-screen">
        <Navbar />
        <div className="mx-auto max-w-7xl px-4 py-20 text-center lg:px-8">
          <h1 className="text-2xl font-bold text-foreground">Doctor Not Found</h1>
          <Link href="/doctors" className="mt-4 inline-block text-primary hover:underline">Back to Doctors</Link>
        </div>
        <Footer />
      </div>
    )
  }

  return (
    <div className="min-h-screen">
      <Navbar />

      {/* Page Header */}
      <section className="bg-card">
        <div className="mx-auto max-w-7xl px-4 py-10 lg:px-8">
          <div className="flex items-center gap-2 text-sm text-muted-foreground">
            <Link href="/" className="hover:text-primary">Home</Link>
            <span>/</span>
            <Link href="/doctors" className="hover:text-primary">Doctors</Link>
            <span>/</span>
            <span className="text-foreground">{doctor.name}</span>
          </div>
        </div>
      </section>

      <div className="mx-auto max-w-7xl px-4 py-10 lg:px-8">
        {/* Doctor Info Top */}
        <div className="mb-10 flex flex-col gap-8 lg:flex-row">
          {/* Photo */}
          <div className="shrink-0">
            <img
              src={doctor.avatar || "/placeholder.svg"}
              alt={doctor.name}
              className="h-64 w-64 rounded-xl object-cover"
            />
          </div>

          {/* Info */}
          <div className="flex-1">
            <h1 className="text-2xl font-bold text-foreground">{doctor.name}</h1>
            <p className="mt-1 text-sm font-medium text-primary">{doctor.qualification}, {doctor.specialty}</p>

            <div className="mt-3 flex items-center gap-2">
              <div className="flex items-center gap-1">
                {Array.from({ length: 5 }).map((_, i) => (
                  <Star key={i} className={`h-4 w-4 ${i < Math.floor(doctor.rating) ? "fill-amber-400 text-amber-400" : "text-border"}`} />
                ))}
              </div>
              <span className="text-sm text-muted-foreground">({doctor.reviews} reviews)</span>
            </div>

            <p className="mt-4 leading-relaxed text-muted-foreground">{doctor.bio}</p>

            <div className="mt-5 flex flex-col gap-2">
              <div className="flex items-center gap-2 text-sm text-muted-foreground">
                <Award className="h-4 w-4 text-primary" />
                <span>Reg No: {doctor.regNo}</span>
              </div>
              <div className="flex items-center gap-2 text-sm text-muted-foreground">
                <MapPin className="h-4 w-4 text-primary" />
                <span>{doctor.location}</span>
              </div>
              <div className="flex items-center gap-2 text-sm text-muted-foreground">
                <Phone className="h-4 w-4 text-primary" />
                <span>Call: {doctor.phone}</span>
              </div>
              <div className="flex items-center gap-2 text-sm text-muted-foreground">
                <Mail className="h-4 w-4 text-primary" />
                <span>Email: {doctor.email}</span>
              </div>
            </div>

            <div className="mt-5 flex items-center gap-3">
              <a href="#" className="flex h-9 w-9 items-center justify-center rounded-full bg-primary/10 text-primary transition-colors hover:bg-primary hover:text-primary-foreground" aria-label="Facebook">
                <Facebook className="h-4 w-4" />
              </a>
              <a href="#" className="flex h-9 w-9 items-center justify-center rounded-full bg-primary/10 text-primary transition-colors hover:bg-primary hover:text-primary-foreground" aria-label="Twitter">
                <Twitter className="h-4 w-4" />
              </a>
              <a href="#" className="flex h-9 w-9 items-center justify-center rounded-full bg-primary/10 text-primary transition-colors hover:bg-primary hover:text-primary-foreground" aria-label="LinkedIn">
                <Linkedin className="h-4 w-4" />
              </a>
              <a href="#" className="flex h-9 w-9 items-center justify-center rounded-full bg-primary/10 text-primary transition-colors hover:bg-primary hover:text-primary-foreground" aria-label="Instagram">
                <Instagram className="h-4 w-4" />
              </a>
            </div>
          </div>

          {/* Appointment Sidebar */}
          <div className="w-full shrink-0 lg:w-80">
            <div className="rounded-xl bg-primary p-6">
              <h3 className="mb-5 text-center text-xl font-bold text-primary-foreground">Make An Appointment</h3>
              <form className="flex flex-col gap-3" onSubmit={(e) => { e.preventDefault() }}>
                <input
                  type="text"
                  placeholder="Name"
                  value={appointmentData.name}
                  onChange={(e) => setAppointmentData({ ...appointmentData, name: e.target.value })}
                  className="w-full rounded-lg bg-primary-foreground px-4 py-2.5 text-sm text-foreground placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-primary-foreground/50"
                />
                <input
                  type="email"
                  placeholder="Email"
                  value={appointmentData.email}
                  onChange={(e) => setAppointmentData({ ...appointmentData, email: e.target.value })}
                  className="w-full rounded-lg bg-primary-foreground px-4 py-2.5 text-sm text-foreground placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-primary-foreground/50"
                />
                <select
                  value={appointmentData.date}
                  onChange={(e) => setAppointmentData({ ...appointmentData, date: e.target.value })}
                  className="w-full rounded-lg bg-primary-foreground px-4 py-2.5 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-primary-foreground/50"
                >
                  <option value="">Select Date</option>
                  <option value="monday">Monday</option>
                  <option value="tuesday">Tuesday</option>
                  <option value="wednesday">Wednesday</option>
                  <option value="thursday">Thursday</option>
                  <option value="friday">Friday</option>
                </select>
                <select
                  value={appointmentData.time}
                  onChange={(e) => setAppointmentData({ ...appointmentData, time: e.target.value })}
                  className="w-full rounded-lg bg-primary-foreground px-4 py-2.5 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-primary-foreground/50"
                >
                  <option value="">Select Time</option>
                  {timeSlots.map((slot) => (
                    <option key={slot} value={slot}>{slot}</option>
                  ))}
                </select>
                <button
                  type="submit"
                  className="mt-2 w-full rounded-lg border-2 border-primary-foreground bg-transparent py-2.5 text-sm font-semibold text-primary-foreground transition-colors hover:bg-primary-foreground hover:text-primary"
                >
                  Appointment
                </button>
              </form>
            </div>
          </div>
        </div>

        {/* Biography */}
        <div className="rounded-xl border border-border bg-card p-6 lg:p-8">
          <h2 className="mb-6 text-xl font-bold text-foreground">Biography Of {doctor.name}</h2>

          <div className="mb-6 border-l-4 border-primary pl-5">
            <h3 className="mb-3 flex items-center gap-2 text-base font-semibold text-foreground">
              <GraduationCap className="h-5 w-5 text-primary" />
              Educational Background
            </h3>
            <p className="leading-relaxed text-muted-foreground">{doctor.education}</p>
          </div>

          <div className="mb-6 border-l-4 border-primary pl-5">
            <h3 className="mb-3 flex items-center gap-2 text-base font-semibold text-foreground">
              <Briefcase className="h-5 w-5 text-primary" />
              Medical Experience & Skills
            </h3>
            <p className="leading-relaxed text-muted-foreground">{doctor.experience}</p>
          </div>

          {/* Working Hours */}
          <div>
            <h3 className="mb-4 flex items-center gap-2 text-base font-semibold text-foreground">
              <Clock className="h-5 w-5 text-primary" />
              Working Hours
            </h3>
            <div className="grid grid-cols-1 gap-2 sm:grid-cols-2 lg:grid-cols-3">
              {doctor.workingHours.map((wh) => (
                <div key={wh.day} className="flex items-center justify-between rounded-lg bg-secondary px-4 py-2.5">
                  <span className="text-sm font-medium text-foreground">{wh.day}</span>
                  <span className="text-sm text-muted-foreground">{wh.time}</span>
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>

      <Footer />
    </div>
  )
}
