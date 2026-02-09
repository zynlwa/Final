"use client"

import React from "react"

import { useState } from "react"
import { Send } from "lucide-react"
import Navbar from "@/components/navbar"
import Footer from "@/components/footer"

const departments = ["Cardiology", "Neurology", "Orthopedic", "Ophthalmology", "Dentistry", "Dermatology", "Gynecology", "Psychotherapy"]
const doctorsList = ["Dr. Calvin Carlo", "Dr. Cristino Murphy", "Dr. Alia Reddy", "Dr. James Moore", "Dr. Sarah Williams", "Dr. Michael Chen"]
const timeSlots = ["09:00 AM", "09:30 AM", "10:00 AM", "10:30 AM", "11:00 AM", "11:30 AM", "02:00 PM", "02:30 PM", "03:00 PM", "03:30 PM", "04:00 PM"]

export default function BookAppointmentPage() {
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    phone: "",
    department: "",
    doctor: "",
    date: "",
    time: "",
    message: "",
  })
  const [submitted, setSubmitted] = useState(false)

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    setSubmitted(true)
    setTimeout(() => setSubmitted(false), 3000)
  }

  return (
    <div className="min-h-screen">
      <Navbar />

      {/* Page Header */}
      <section className="bg-card">
        <div className="mx-auto max-w-7xl px-4 py-10 lg:px-8">
          <h1 className="text-3xl font-bold text-foreground">Book Appointment</h1>
          <p className="mt-2 text-muted-foreground">Schedule your appointment with our medical experts</p>
        </div>
      </section>

      <div className="mx-auto max-w-7xl px-4 py-10 lg:px-8">
        <div className="flex flex-col items-center gap-12 lg:flex-row lg:items-start">
          {/* Image */}
          <div className="hidden w-full max-w-md lg:block">
            <img
              src="https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=500&h=700&fit=crop"
              alt="Medical professional"
              className="h-auto w-full rounded-2xl object-cover"
            />
          </div>

          {/* Form */}
          <div className="w-full flex-1">
            <h2 className="mb-2 text-2xl font-bold text-foreground">Book Appointment</h2>
            <p className="mb-8 text-sm leading-relaxed text-muted-foreground">
              Fill in the form below to book an appointment with one of our specialists. We will get back to you as soon as possible.
            </p>

            {submitted && (
              <div className="mb-6 rounded-lg bg-emerald-50 p-4 text-sm font-medium text-emerald-700">
                Your appointment request has been sent successfully! We will contact you shortly.
              </div>
            )}

            <form onSubmit={handleSubmit} className="flex flex-col gap-5">
              {/* Patient Name */}
              <input
                type="text"
                placeholder="Patient Name*"
                required
                value={formData.name}
                onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                className="w-full rounded-xl border border-border bg-primary/5 px-4 py-3 text-sm text-foreground placeholder:text-muted-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"
              />

              {/* Email & Phone */}
              <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                <input
                  type="email"
                  placeholder="Email*"
                  required
                  value={formData.email}
                  onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                  className="w-full rounded-xl border border-border bg-primary/5 px-4 py-3 text-sm text-foreground placeholder:text-muted-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"
                />
                <input
                  type="tel"
                  placeholder="Phone*"
                  required
                  value={formData.phone}
                  onChange={(e) => setFormData({ ...formData, phone: e.target.value })}
                  className="w-full rounded-xl border border-border bg-primary/5 px-4 py-3 text-sm text-foreground placeholder:text-muted-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"
                />
              </div>

              {/* Department & Doctor */}
              <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                <select
                  value={formData.department}
                  onChange={(e) => setFormData({ ...formData, department: e.target.value })}
                  className="w-full rounded-xl border border-border bg-primary/5 px-4 py-3 text-sm text-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"
                >
                  <option value="">Select Department</option>
                  {departments.map((dep) => (
                    <option key={dep} value={dep}>{dep}</option>
                  ))}
                </select>
                <select
                  value={formData.doctor}
                  onChange={(e) => setFormData({ ...formData, doctor: e.target.value })}
                  className="w-full rounded-xl border border-border bg-primary/5 px-4 py-3 text-sm text-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"
                >
                  <option value="">Select Doctor</option>
                  {doctorsList.map((doc) => (
                    <option key={doc} value={doc}>{doc}</option>
                  ))}
                </select>
              </div>

              {/* Date & Time */}
              <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                <input
                  type="date"
                  value={formData.date}
                  onChange={(e) => setFormData({ ...formData, date: e.target.value })}
                  className="w-full rounded-xl border border-border bg-primary/5 px-4 py-3 text-sm text-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"
                />
                <select
                  value={formData.time}
                  onChange={(e) => setFormData({ ...formData, time: e.target.value })}
                  className="w-full rounded-xl border border-border bg-primary/5 px-4 py-3 text-sm text-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"
                >
                  <option value="">Select Time</option>
                  {timeSlots.map((slot) => (
                    <option key={slot} value={slot}>{slot}</option>
                  ))}
                </select>
              </div>

              {/* Message */}
              <textarea
                placeholder="Message"
                rows={4}
                value={formData.message}
                onChange={(e) => setFormData({ ...formData, message: e.target.value })}
                className="w-full rounded-xl border border-border bg-primary/5 px-4 py-3 text-sm text-foreground placeholder:text-muted-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"
              />

              <button
                type="submit"
                className="flex w-full items-center justify-center gap-2 rounded-xl bg-primary py-3 text-sm font-semibold text-primary-foreground transition-opacity hover:opacity-90 sm:w-auto sm:px-8"
              >
                <Send className="h-4 w-4" />
                Book Appointment
              </button>
            </form>
          </div>
        </div>
      </div>

      <Footer />
    </div>
  )
}
