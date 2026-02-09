import Link from "next/link"
import { Star, MapPin, Calendar, Facebook, Linkedin, Twitter, Instagram } from "lucide-react"
import { Card, CardContent } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import Navbar from "@/components/navbar"
import Footer from "@/components/footer"

const doctors = [
  {
    name: "Dr. Calvin Carlo",
    specialty: "Eye Care",
    qualification: "M.B.B.S",
    location: "New York, USA",
    rating: 4.9,
    reviews: 128,
    slug: "dr-calvin-carlo",
    avatar: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=300&h=350&fit=crop&crop=face",
    available: true,
  },
  {
    name: "Dr. Cristino Murphy",
    specialty: "Gynecologist",
    qualification: "M.B.B.S",
    location: "London, UK",
    rating: 4.8,
    reviews: 95,
    slug: "dr-cristino-murphy",
    avatar: "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=300&h=350&fit=crop&crop=face",
    available: true,
  },
  {
    name: "Dr. Alia Reddy",
    specialty: "Psychotherapist",
    qualification: "M.B.B.S",
    location: "Sydney, Australia",
    rating: 4.7,
    reviews: 82,
    slug: "dr-alia-reddy",
    avatar: "https://images.unsplash.com/photo-1594824476967-48c8b964ac31?w=300&h=350&fit=crop&crop=face",
    available: false,
  },
  {
    name: "Dr. James Moore",
    specialty: "Orthopedic",
    qualification: "M.B.B.S",
    location: "Toronto, Canada",
    rating: 4.9,
    reviews: 156,
    slug: "dr-james-moore",
    avatar: "https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=300&h=350&fit=crop&crop=face",
    available: true,
  },
  {
    name: "Dr. Sarah Williams",
    specialty: "Dermatology",
    qualification: "M.B.B.S, M.D",
    location: "Berlin, Germany",
    rating: 4.6,
    reviews: 67,
    slug: "dr-sarah-williams",
    avatar: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=300&h=350&fit=crop&crop=face",
    available: true,
  },
  {
    name: "Dr. Michael Chen",
    specialty: "Ophthalmology",
    qualification: "M.B.B.S, M.S",
    location: "Tokyo, Japan",
    rating: 4.8,
    reviews: 110,
    slug: "dr-michael-chen",
    avatar: "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=300&h=350&fit=crop&crop=face",
    available: true,
  },
]

export default function DoctorsPage() {
  return (
    <div className="min-h-screen">
      <Navbar />

      <div className="mx-auto max-w-7xl px-4 py-10 lg:px-8">
        <div className="mb-10">
          <h1 className="text-3xl font-bold text-foreground">Our Doctors</h1>
          <p className="mt-2 text-muted-foreground">
            Find and book appointments with our top healthcare professionals
          </p>
        </div>

        <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-4">
          {doctors.map((doc) => (
            <Card key={doc.slug} className="group overflow-hidden border-border shadow-sm transition-all hover:shadow-lg">
              <CardContent className="relative flex flex-col items-center p-0 text-center">
                {/* Image area */}
                <Link href={`/doctors/${doc.slug}`} className="relative block w-full overflow-hidden">
                  <img
                    src={doc.avatar || "/placeholder.svg"}
                    alt={doc.name}
                    className="h-64 w-full object-cover object-top transition-transform group-hover:scale-105"
                  />
                  {/* Social icons on hover */}
                  <div className="absolute right-3 top-3 flex flex-col gap-2 opacity-0 transition-opacity group-hover:opacity-100">
                    <a href="#" className="flex h-8 w-8 items-center justify-center rounded-full bg-card/90 text-primary shadow-sm transition-colors hover:bg-primary hover:text-primary-foreground" aria-label="Facebook">
                      <Facebook className="h-3.5 w-3.5" />
                    </a>
                    <a href="#" className="flex h-8 w-8 items-center justify-center rounded-full bg-card/90 text-primary shadow-sm transition-colors hover:bg-primary hover:text-primary-foreground" aria-label="LinkedIn">
                      <Linkedin className="h-3.5 w-3.5" />
                    </a>
                    <a href="#" className="flex h-8 w-8 items-center justify-center rounded-full bg-card/90 text-primary shadow-sm transition-colors hover:bg-primary hover:text-primary-foreground" aria-label="Twitter">
                      <Twitter className="h-3.5 w-3.5" />
                    </a>
                    <a href="#" className="flex h-8 w-8 items-center justify-center rounded-full bg-card/90 text-primary shadow-sm transition-colors hover:bg-primary hover:text-primary-foreground" aria-label="Instagram">
                      <Instagram className="h-3.5 w-3.5" />
                    </a>
                  </div>
                </Link>

                {/* Info area - shown on hover with primary bg */}
                <div className="w-full p-5 transition-colors group-hover:bg-primary">
                  <Link href={`/doctors/${doc.slug}`}>
                    <h3 className="text-lg font-semibold text-foreground transition-colors group-hover:text-primary-foreground">{doc.name}</h3>
                  </Link>
                  <p className="mt-1 text-sm text-muted-foreground transition-colors group-hover:text-primary-foreground/80">
                    {doc.qualification}, {doc.specialty}
                  </p>
                </div>
              </CardContent>
            </Card>
          ))}
        </div>

        {/* Book Appointment CTA */}
        <div className="mt-12 text-center">
          <Link
            href="/book-appointment"
            className="inline-flex items-center gap-2 rounded-xl bg-primary px-8 py-3 text-sm font-semibold text-primary-foreground transition-opacity hover:opacity-90"
          >
            <Calendar className="h-4 w-4" />
            Book Appointment
          </Link>
        </div>
      </div>

      <Footer />
    </div>
  )
}
