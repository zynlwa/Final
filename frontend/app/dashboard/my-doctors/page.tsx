import Link from "next/link"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Star, MapPin, Phone } from "lucide-react"

const myDoctors = [
  { name: "Dr. Calvin Carlo", specialty: "Orthopedic", rating: 4.8, visits: 3, phone: "+1 (700) 230-0035", location: "California, USA", slug: "calvin-carlo", avatar: "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=200&h=200&fit=crop&crop=face" },
  { name: "Dr. Cristino Murphy", specialty: "Gynecologist", rating: 4.9, visits: 2, phone: "+1 (700) 230-0036", location: "New York, USA", slug: "cristino-murphy", avatar: "https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=200&h=200&fit=crop&crop=face" },
  { name: "Dr. Alia Reddy", specialty: "Psychotherapist", rating: 4.7, visits: 1, phone: "+1 (700) 230-0037", location: "Texas, USA", slug: "alia-reddy", avatar: "https://images.unsplash.com/photo-1594824476967-48c8b964ac31?w=200&h=200&fit=crop&crop=face" },
]

export default function MyDoctorsPage() {
  return (
    <div className="flex flex-col gap-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-foreground">My Doctors</h1>
          <p className="text-sm text-muted-foreground">Doctors you have visited</p>
        </div>
        <Link href="/doctors" className="rounded-lg bg-primary px-5 py-2.5 text-sm font-medium text-primary-foreground transition-opacity hover:opacity-90">
          Find Doctors
        </Link>
      </div>

      <div className="grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
        {myDoctors.map((doc) => (
          <Card key={doc.slug} className="border-border shadow-sm transition-shadow hover:shadow-md">
            <CardContent className="p-5">
              <div className="mb-4 flex items-center gap-4">
                <Avatar className="h-14 w-14">
                  <AvatarImage src={doc.avatar || "/placeholder.svg"} alt={doc.name} />
                  <AvatarFallback>{doc.name.charAt(4)}</AvatarFallback>
                </Avatar>
                <div>
                  <h3 className="text-sm font-semibold text-foreground">{doc.name}</h3>
                  <p className="text-xs text-primary">{doc.specialty}</p>
                  <div className="mt-1 flex items-center gap-1">
                    <Star className="h-3 w-3 fill-amber-400 text-amber-400" />
                    <span className="text-xs font-medium text-foreground">{doc.rating}</span>
                    <span className="text-xs text-muted-foreground">({doc.visits} visits)</span>
                  </div>
                </div>
              </div>
              <div className="mb-4 flex flex-col gap-2">
                <div className="flex items-center gap-2 text-xs text-muted-foreground">
                  <MapPin className="h-3 w-3" />
                  {doc.location}
                </div>
                <div className="flex items-center gap-2 text-xs text-muted-foreground">
                  <Phone className="h-3 w-3" />
                  {doc.phone}
                </div>
              </div>
              <div className="flex gap-2">
                <Link href={`/doctors/${doc.slug}`} className="flex-1 rounded-lg border border-border bg-transparent py-2 text-center text-xs font-medium text-foreground transition-colors hover:bg-secondary">
                  View Profile
                </Link>
                <Link href="/book-appointment" className="flex-1 rounded-lg bg-primary py-2 text-center text-xs font-medium text-primary-foreground transition-opacity hover:opacity-90">
                  Book Again
                </Link>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>
    </div>
  )
}
