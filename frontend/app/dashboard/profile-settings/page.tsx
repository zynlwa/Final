"use client"

import React from "react"
import { useState } from "react"
import { Upload, X } from "lucide-react"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Textarea } from "@/components/ui/textarea"
import { useAuth } from "@/context/auth-context"

function DoctorSettings() {
  const [form, setForm] = useState({
    firstName: "Calvin",
    lastName: "Carlo",
    email: "calvin@example.com",
    phone: "+(125) 458-8547",
    specialty: "Orthopedic",
    bio: "Experienced orthopedic specialist with over 15 years in the field. Committed to providing excellent patient care and staying current with the latest medical advancements.",
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Profile Settings</h1>
        <p className="text-sm text-muted-foreground">Manage your doctor profile information</p>
      </div>

      <Card className="border-border shadow-sm">
        <CardHeader>
          <CardTitle className="text-lg font-semibold text-foreground">Personal Information :</CardTitle>
        </CardHeader>
        <CardContent className="flex flex-col gap-8">
          <div className="flex flex-col items-start gap-4 rounded-xl border border-border p-4 sm:flex-row sm:items-center">
            <Avatar className="h-16 w-16">
              <AvatarImage src="https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=100&h=100&fit=crop&crop=face" alt="Profile" />
              <AvatarFallback>DC</AvatarFallback>
            </Avatar>
            <div className="flex-1">
              <p className="text-sm font-semibold text-foreground">Upload your picture</p>
              <p className="text-xs leading-relaxed text-muted-foreground">For best results, use an image at least 256px by 256px in either .jpg or .png format</p>
            </div>
            <div className="flex gap-2">
              <button className="flex items-center gap-2 rounded-lg bg-primary px-5 py-2.5 text-sm font-medium text-primary-foreground transition-opacity hover:opacity-90">
                <Upload className="h-4 w-4" />
                Upload
              </button>
              <button className="flex items-center gap-2 rounded-lg border border-border bg-transparent px-5 py-2.5 text-sm font-medium text-muted-foreground transition-colors hover:bg-secondary hover:text-foreground">
                <X className="h-4 w-4" />
                Remove
              </button>
            </div>
          </div>

          <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
            <div className="flex flex-col gap-2">
              <Label htmlFor="firstName" className="text-sm font-medium text-foreground">First Name</Label>
              <Input id="firstName" name="firstName" value={form.firstName} onChange={handleChange} placeholder="First Name :" className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="lastName" className="text-sm font-medium text-foreground">Last Name</Label>
              <Input id="lastName" name="lastName" value={form.lastName} onChange={handleChange} placeholder="Last Name :" className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="email" className="text-sm font-medium text-foreground">Your Email</Label>
              <Input id="email" name="email" type="email" value={form.email} onChange={handleChange} placeholder="Your email :" className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="phone" className="text-sm font-medium text-foreground">Phone no.</Label>
              <Input id="phone" name="phone" value={form.phone} onChange={handleChange} placeholder="Phone no. :" className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2 sm:col-span-2">
              <Label htmlFor="specialty" className="text-sm font-medium text-foreground">Specialty</Label>
              <Input id="specialty" name="specialty" value={form.specialty} onChange={handleChange} placeholder="Specialty :" className="border-border bg-card" />
            </div>
          </div>

          <div className="flex flex-col gap-2">
            <Label htmlFor="bio" className="text-sm font-medium text-foreground">Your Bio Here</Label>
            <Textarea id="bio" name="bio" value={form.bio} onChange={handleChange} placeholder="Bio :" rows={5} className="resize-y border-border bg-card" />
          </div>

          <div>
            <button className="rounded-lg bg-primary px-6 py-2.5 text-sm font-medium text-primary-foreground transition-opacity hover:opacity-90">Save changes</button>
          </div>
        </CardContent>
      </Card>
    </div>
  )
}

function PatientSettings() {
  const [form, setForm] = useState({
    firstName: "Christopher",
    lastName: "Burrell",
    email: "christopher@example.com",
    phone: "+(125) 458-8547",
    birthday: "1993-09-13",
    gender: "Female",
    address: "Sydney, Australia",
    bloodGroup: "B+",
    bio: "I am a 25-year-old living in Sydney, Australia. I have been a regular patient at MediCare for the past 3 years and I appreciate the quality healthcare services provided.",
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Profile Settings</h1>
        <p className="text-sm text-muted-foreground">Manage your patient profile information</p>
      </div>

      <Card className="border-border shadow-sm">
        <CardHeader>
          <CardTitle className="text-lg font-semibold text-foreground">Personal Information :</CardTitle>
        </CardHeader>
        <CardContent className="flex flex-col gap-8">
          <div className="flex flex-col items-start gap-4 rounded-xl border border-border p-4 sm:flex-row sm:items-center">
            <Avatar className="h-16 w-16">
              <AvatarImage src="https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=100&h=100&fit=crop&crop=face" alt="Profile" />
              <AvatarFallback>CB</AvatarFallback>
            </Avatar>
            <div className="flex-1">
              <p className="text-sm font-semibold text-foreground">Upload your picture</p>
              <p className="text-xs leading-relaxed text-muted-foreground">For best results, use an image at least 256px by 256px in either .jpg or .png format</p>
            </div>
            <div className="flex gap-2">
              <button className="flex items-center gap-2 rounded-lg bg-primary px-5 py-2.5 text-sm font-medium text-primary-foreground transition-opacity hover:opacity-90">
                <Upload className="h-4 w-4" />
                Upload
              </button>
              <button className="flex items-center gap-2 rounded-lg border border-border bg-transparent px-5 py-2.5 text-sm font-medium text-muted-foreground transition-colors hover:bg-secondary hover:text-foreground">
                <X className="h-4 w-4" />
                Remove
              </button>
            </div>
          </div>

          <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
            <div className="flex flex-col gap-2">
              <Label htmlFor="firstName" className="text-sm font-medium text-foreground">First Name</Label>
              <Input id="firstName" name="firstName" value={form.firstName} onChange={handleChange} placeholder="First Name :" className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="lastName" className="text-sm font-medium text-foreground">Last Name</Label>
              <Input id="lastName" name="lastName" value={form.lastName} onChange={handleChange} placeholder="Last Name :" className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="email" className="text-sm font-medium text-foreground">Your Email</Label>
              <Input id="email" name="email" type="email" value={form.email} onChange={handleChange} placeholder="Your email :" className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="phone" className="text-sm font-medium text-foreground">Phone no.</Label>
              <Input id="phone" name="phone" value={form.phone} onChange={handleChange} placeholder="Phone no. :" className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="birthday" className="text-sm font-medium text-foreground">Birthday</Label>
              <Input id="birthday" name="birthday" type="date" value={form.birthday} onChange={handleChange} className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="gender" className="text-sm font-medium text-foreground">Gender</Label>
              <select id="gender" name="gender" value={form.gender} onChange={handleChange} className="rounded-md border border-border bg-card px-3 py-2 text-sm text-foreground">
                <option value="Male">Male</option>
                <option value="Female">Female</option>
                <option value="Other">Other</option>
              </select>
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="address" className="text-sm font-medium text-foreground">Address</Label>
              <Input id="address" name="address" value={form.address} onChange={handleChange} placeholder="Address :" className="border-border bg-card" />
            </div>
            <div className="flex flex-col gap-2">
              <Label htmlFor="bloodGroup" className="text-sm font-medium text-foreground">Blood Group</Label>
              <Input id="bloodGroup" name="bloodGroup" value={form.bloodGroup} onChange={handleChange} placeholder="Blood Group :" className="border-border bg-card" />
            </div>
          </div>

          <div className="flex flex-col gap-2">
            <Label htmlFor="bio" className="text-sm font-medium text-foreground">Your Bio Here</Label>
            <Textarea id="bio" name="bio" value={form.bio} onChange={handleChange} placeholder="Bio :" rows={5} className="resize-y border-border bg-card" />
          </div>

          <div>
            <button className="rounded-lg bg-primary px-6 py-2.5 text-sm font-medium text-primary-foreground transition-opacity hover:opacity-90">Save changes</button>
          </div>
        </CardContent>
      </Card>
    </div>
  )
}

export default function ProfileSettingsPage() {
  const { role } = useAuth()

  if (role === "patient") {
    return <PatientSettings />
  }
  return <DoctorSettings />
}
