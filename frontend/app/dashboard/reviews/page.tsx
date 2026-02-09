import { Star } from "lucide-react"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"

const reviews = [
  {
    name: "Christopher Burrell",
    rating: 5,
    date: "13 Mar 2026",
    comment: "Dr. Calvin is an excellent orthopedic specialist. He explained everything clearly and I felt very comfortable during the whole procedure. Highly recommended!",
    avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=80&h=80&fit=crop&crop=face",
  },
  {
    name: "Sarah Johnson",
    rating: 4,
    date: "14 Mar 2026",
    comment: "Great experience overall. The clinic is clean, staff is friendly, and the doctor is very knowledgeable. Wait time was a bit longer than expected.",
    avatar: "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=80&h=80&fit=crop&crop=face",
  },
  {
    name: "Michael Smith",
    rating: 5,
    date: "15 Mar 2026",
    comment: "Best doctor I have visited in a long time. Very thorough examination and accurate diagnosis. I am feeling much better after following his treatment plan.",
    avatar: "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=80&h=80&fit=crop&crop=face",
  },
  {
    name: "Emily Davis",
    rating: 4,
    date: "16 Mar 2026",
    comment: "Professional and caring doctor. He took time to listen to my concerns and provided personalized treatment options. The staff was also very helpful.",
    avatar: "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=80&h=80&fit=crop&crop=face",
  },
]

export default function ReviewsPage() {
  const avgRating = (reviews.reduce((sum, r) => sum + r.rating, 0) / reviews.length).toFixed(1)

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Patient Reviews</h1>
        <p className="text-sm text-muted-foreground">See what patients say about you</p>
      </div>

      <div className="flex items-center gap-4 rounded-xl border border-border bg-card p-5">
        <div className="flex h-16 w-16 items-center justify-center rounded-xl bg-primary/10">
          <Star className="h-8 w-8 fill-amber-400 text-amber-400" />
        </div>
        <div>
          <p className="text-3xl font-bold text-foreground">{avgRating}</p>
          <div className="flex items-center gap-1">
            {Array.from({ length: 5 }).map((_, i) => (
              <Star
                key={i}
                className={`h-4 w-4 ${i < Math.round(Number(avgRating)) ? "fill-amber-400 text-amber-400" : "text-border"}`}
              />
            ))}
            <span className="ml-1 text-sm text-muted-foreground">({reviews.length} reviews)</span>
          </div>
        </div>
      </div>

      <div className="flex flex-col gap-4">
        {reviews.map((review) => (
          <Card key={review.name} className="border-border shadow-sm">
            <CardContent className="p-5">
              <div className="flex items-start gap-4">
                <Avatar className="h-11 w-11">
                  <AvatarImage src={review.avatar || "/placeholder.svg"} alt={review.name} />
                  <AvatarFallback>{review.name[0]}</AvatarFallback>
                </Avatar>
                <div className="flex-1">
                  <div className="flex flex-wrap items-center justify-between gap-2">
                    <h3 className="text-sm font-semibold text-foreground">{review.name}</h3>
                    <span className="text-xs text-muted-foreground">{review.date}</span>
                  </div>
                  <div className="mb-2 mt-1 flex items-center gap-0.5">
                    {Array.from({ length: 5 }).map((_, i) => (
                      <Star
                        key={i}
                        className={`h-3.5 w-3.5 ${i < review.rating ? "fill-amber-400 text-amber-400" : "text-border"}`}
                      />
                    ))}
                  </div>
                  <p className="text-sm leading-relaxed text-muted-foreground">{review.comment}</p>
                </div>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>
    </div>
  )
}
