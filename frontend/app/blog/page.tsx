import Link from "next/link"
import { Calendar, User, ArrowRight, Search, Tag } from "lucide-react"
import Navbar from "@/components/navbar"
import Footer from "@/components/footer"

const blogPosts = [
  {
    id: "consultant-business",
    title: "Consultant Business",
    excerpt: "This is required when, for example, the final text is not yet available. Dummy text is also known as 'fill text'. It is said that song composers of the past used dummy texts.",
    image: "https://images.unsplash.com/photo-1576091160399-112ba8d25d1d?w=600&h=400&fit=crop",
    author: "Dr. Calvin Carlo",
    date: "13th Sep 2025",
    category: "Business",
  },
  {
    id: "look-on-the-glorious-balance",
    title: "Look On The Glorious Balance",
    excerpt: "Due to its widespread use as filler text for layouts, non-readability is of great importance: human perception is tuned to recognize certain patterns and repetitions in texts.",
    image: "https://images.unsplash.com/photo-1579684385127-1ef15d508118?w=600&h=400&fit=crop",
    author: "Dr. Alia Reddy",
    date: "29th Nov 2025",
    category: "Health",
  },
  {
    id: "research-financial",
    title: "Research Financial",
    excerpt: "For this reason, dummy text usually consists of a more or less random series of words or syllables. This prevents repetitive patterns from distracting the reader.",
    image: "https://images.unsplash.com/photo-1551076805-e1869033e561?w=600&h=400&fit=crop",
    author: "Dr. Cristino Murphy",
    date: "29th Dec 2025",
    category: "Finance",
  },
  {
    id: "tips-for-healthy-living",
    title: "Tips For Healthy Living",
    excerpt: "Maintaining a healthy lifestyle requires consistent effort in diet, exercise, and mental well-being. Learn the best practices from our medical experts.",
    image: "https://images.unsplash.com/photo-1505751172876-fa1923c5c528?w=600&h=400&fit=crop",
    author: "Dr. Sarah Williams",
    date: "5th Jan 2026",
    category: "Lifestyle",
  },
  {
    id: "modern-medical-technology",
    title: "Modern Medical Technology",
    excerpt: "Advances in medical technology have revolutionized how we approach diagnosis and treatment. From AI to robotics, the future of healthcare is here.",
    image: "https://images.unsplash.com/photo-1530497610245-94d3c16cda28?w=600&h=400&fit=crop",
    author: "Dr. James Moore",
    date: "15th Jan 2026",
    category: "Technology",
  },
  {
    id: "nutrition-and-wellness",
    title: "Nutrition and Wellness Guide",
    excerpt: "Proper nutrition plays a crucial role in maintaining overall health. Our comprehensive guide covers everything from macronutrients to meal planning.",
    image: "https://images.unsplash.com/photo-1532938911079-1b06ac7ceec7?w=600&h=400&fit=crop",
    author: "Dr. Michael Chen",
    date: "22nd Jan 2026",
    category: "Health",
  },
]

const recentPosts = blogPosts.slice(0, 3)

const tags = ["BUSINESS", "FINANCE", "MARKETING", "HEALTH", "LIFESTYLE", "TRAVEL", "BEAUTY", "TECHNOLOGY"]

export default function BlogPage() {
  return (
    <div className="min-h-screen">
      <Navbar />

      {/* Page Header */}
      <section className="bg-card">
        <div className="mx-auto max-w-7xl px-4 py-10 lg:px-8">
          <h1 className="text-3xl font-bold text-foreground">Blog</h1>
          <p className="mt-2 text-muted-foreground">Latest news and health tips from our medical experts</p>
        </div>
      </section>

      <div className="mx-auto max-w-7xl px-4 py-10 lg:px-8">
        <div className="flex flex-col gap-10 lg:flex-row">
          {/* Blog Posts Grid */}
          <div className="flex-1">
            <div className="grid grid-cols-1 gap-8 md:grid-cols-2">
              {blogPosts.map((post) => (
                <article key={post.id} className="overflow-hidden rounded-xl border border-border bg-card transition-shadow hover:shadow-md">
                  <Link href={`/blog/${post.id}`}>
                    <img src={post.image || "/placeholder.svg"} alt={post.title} className="h-52 w-full object-cover" />
                  </Link>
                  <div className="p-5">
                    <div className="mb-3 flex items-center gap-4 text-xs text-muted-foreground">
                      <span className="flex items-center gap-1">
                        <User className="h-3.5 w-3.5" />
                        {post.author}
                      </span>
                      <span className="flex items-center gap-1">
                        <Calendar className="h-3.5 w-3.5" />
                        {post.date}
                      </span>
                    </div>
                    <Link href={`/blog/${post.id}`}>
                      <h2 className="mb-2 text-lg font-semibold text-foreground transition-colors hover:text-primary">
                        {post.title}
                      </h2>
                    </Link>
                    <p className="mb-4 line-clamp-3 text-sm leading-relaxed text-muted-foreground">
                      {post.excerpt}
                    </p>
                    <Link
                      href={`/blog/${post.id}`}
                      className="inline-flex items-center gap-1.5 text-sm font-medium text-primary transition-colors hover:text-primary/80"
                    >
                      Read More
                      <ArrowRight className="h-3.5 w-3.5" />
                    </Link>
                  </div>
                </article>
              ))}
            </div>
          </div>

          {/* Sidebar */}
          <aside className="w-full shrink-0 lg:w-80">
            {/* Search */}
            <div className="mb-8 rounded-xl border border-border bg-card p-5">
              <h3 className="mb-4 text-lg font-semibold text-foreground">Search</h3>
              <div className="relative">
                <input
                  type="text"
                  placeholder="Search Keywords..."
                  className="w-full rounded-lg border border-border bg-background px-4 py-2.5 pr-10 text-sm text-foreground placeholder:text-muted-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary"
                />
                <Search className="absolute right-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
              </div>
            </div>

            {/* Recent Posts */}
            <div className="mb-8 rounded-xl border border-border bg-card p-5">
              <h3 className="mb-4 text-lg font-semibold text-foreground">Recent Post</h3>
              <div className="flex flex-col gap-4">
                {recentPosts.map((post) => (
                  <Link key={post.id} href={`/blog/${post.id}`} className="group flex items-center gap-3">
                    <img src={post.image || "/placeholder.svg"} alt={post.title} className="h-16 w-16 shrink-0 rounded-lg object-cover" />
                    <div>
                      <h4 className="text-sm font-medium text-foreground transition-colors group-hover:text-primary">{post.title}</h4>
                      <span className="text-xs text-muted-foreground">{post.date}</span>
                    </div>
                  </Link>
                ))}
              </div>
            </div>

            {/* Tags Cloud */}
            <div className="rounded-xl border border-border bg-card p-5">
              <h3 className="mb-4 text-lg font-semibold text-foreground">Tags Cloud</h3>
              <div className="flex flex-wrap gap-2">
                {tags.map((tag) => (
                  <span key={tag} className="flex items-center gap-1 rounded-md bg-secondary px-3 py-1.5 text-xs font-medium text-muted-foreground transition-colors hover:bg-primary hover:text-primary-foreground">
                    <Tag className="h-3 w-3" />
                    {tag}
                  </span>
                ))}
              </div>
            </div>
          </aside>
        </div>
      </div>

      <Footer />
    </div>
  )
}
