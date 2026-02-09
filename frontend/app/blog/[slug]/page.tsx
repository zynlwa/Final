import Link from "next/link"
import { Calendar, User, ArrowRight, Search, Tag, Reply } from "lucide-react"
import Navbar from "@/components/navbar"
import Footer from "@/components/footer"

const blogPosts: Record<string, {
  title: string
  image: string
  author: string
  authorAvatar: string
  date: string
  category: string
  content: string[]
}> = {
  "consultant-business": {
    title: "Consultant Business",
    image: "https://images.unsplash.com/photo-1576091160399-112ba8d25d1d?w=900&h=500&fit=crop",
    author: "Dr. Calvin Carlo",
    authorAvatar: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=100&h=100&fit=crop&crop=face",
    date: "13th Sep 2025",
    category: "Business",
    content: [
      "This is required when, for example, the final text is not yet available. Dummy text is also known as 'fill text'. It is said that song composers of the past used dummy texts as lyrics when writing melodies in order to have a 'ready-made' text to sing with the melody. Dummy texts have been in use by typesetters since the 16th century.",
      "Due to its widespread use as filler text for layouts, non-readability is of great importance: human perception is tuned to recognize certain patterns and repetitions in texts.",
      "For this reason, dummy text usually consists of a more or less random series of words or syllables.",
    ],
  },
  "look-on-the-glorious-balance": {
    title: "Look On The Glorious Balance",
    image: "https://images.unsplash.com/photo-1579684385127-1ef15d508118?w=900&h=500&fit=crop",
    author: "Dr. Alia Reddy",
    authorAvatar: "https://images.unsplash.com/photo-1594824476967-48c8b964ac31?w=100&h=100&fit=crop&crop=face",
    date: "29th Nov 2025",
    category: "Health",
    content: [
      "Due to its widespread use as filler text for layouts, non-readability is of great importance: human perception is tuned to recognize certain patterns and repetitions in texts.",
      "The most well-known dummy text is the Lorem Ipsum, which is said to have originated in the 16th century. It has survived not only five centuries, but also the leap into electronic typesetting.",
      "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software.",
    ],
  },
  "research-financial": {
    title: "Research Financial",
    image: "https://images.unsplash.com/photo-1551076805-e1869033e561?w=900&h=500&fit=crop",
    author: "Dr. Cristino Murphy",
    authorAvatar: "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=100&h=100&fit=crop&crop=face",
    date: "29th Dec 2025",
    category: "Finance",
    content: [
      "For this reason, dummy text usually consists of a more or less random series of words or syllables. This prevents repetitive patterns from distracting the reader.",
      "The advantage of using Latin text is that its structure approximates natural language, making it appear as a plausible block of text without distracting from the layout.",
      "Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text.",
    ],
  },
  "tips-for-healthy-living": {
    title: "Tips For Healthy Living",
    image: "https://images.unsplash.com/photo-1505751172876-fa1923c5c528?w=900&h=500&fit=crop",
    author: "Dr. Sarah Williams",
    authorAvatar: "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=100&h=100&fit=crop&crop=face",
    date: "5th Jan 2026",
    category: "Lifestyle",
    content: [
      "Maintaining a healthy lifestyle requires consistent effort in diet, exercise, and mental well-being. Learn the best practices from our medical experts.",
      "Regular physical activity is one of the most important things you can do for your health. Being physically active can improve your brain health and help manage weight.",
      "A balanced diet provides your body with the nutrients it needs to function correctly. To get the proper nutrition, most of your daily calories should come from fresh fruits, vegetables, and whole grains.",
    ],
  },
  "modern-medical-technology": {
    title: "Modern Medical Technology",
    image: "https://images.unsplash.com/photo-1530497610245-94d3c16cda28?w=900&h=500&fit=crop",
    author: "Dr. James Moore",
    authorAvatar: "https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=100&h=100&fit=crop&crop=face",
    date: "15th Jan 2026",
    category: "Technology",
    content: [
      "Advances in medical technology have revolutionized how we approach diagnosis and treatment. From AI to robotics, the future of healthcare is here.",
      "Telemedicine has become an essential tool, allowing patients to consult with doctors remotely. This technology has proven especially valuable for follow-up appointments.",
      "Artificial intelligence and machine learning are being used to analyze medical images, predict patient outcomes, and develop personalized treatment plans.",
    ],
  },
  "nutrition-and-wellness": {
    title: "Nutrition and Wellness Guide",
    image: "https://images.unsplash.com/photo-1532938911079-1b06ac7ceec7?w=900&h=500&fit=crop",
    author: "Dr. Michael Chen",
    authorAvatar: "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=100&h=100&fit=crop&crop=face",
    date: "22nd Jan 2026",
    category: "Health",
    content: [
      "Proper nutrition plays a crucial role in maintaining overall health. Our comprehensive guide covers everything from macronutrients to meal planning.",
      "Understanding the role of vitamins and minerals in your diet is essential. Each nutrient serves a specific function and contributes to your overall well-being.",
      "Meal planning can help you maintain a balanced diet while saving time and reducing food waste. Start with simple recipes and gradually expand your repertoire.",
    ],
  },
}

const recentPosts = [
  { id: "consultant-business", title: "Consultant Business", date: "13th Sep 2025", image: "https://images.unsplash.com/photo-1576091160399-112ba8d25d1d?w=100&h=100&fit=crop" },
  { id: "look-on-the-glorious-balance", title: "Look On The Glorious Balance", date: "29th Nov 2025", image: "https://images.unsplash.com/photo-1579684385127-1ef15d508118?w=100&h=100&fit=crop" },
  { id: "research-financial", title: "Research Financial", date: "29th Dec 2025", image: "https://images.unsplash.com/photo-1551076805-e1869033e561?w=100&h=100&fit=crop" },
]

const tags = ["BUSINESS", "FINANCE", "MARKETING", "HEALTH", "LIFESTYLE", "TRAVEL", "BEAUTY", "TECHNOLOGY"]

const comments = [
  {
    name: "Lorenzo Peterson",
    date: "13th March 2025 at 01:25pm",
    avatar: "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=80&h=80&fit=crop&crop=face",
    text: "This is a very informative article. I appreciate the detailed analysis and the practical advice provided. Looking forward to more content like this.",
  },
  {
    name: "Sarah Thompson",
    date: "15th March 2025 at 10:30am",
    avatar: "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=80&h=80&fit=crop&crop=face",
    text: "Great read! The insights shared here are really valuable for anyone in the healthcare field. Thank you for sharing your expertise.",
  },
]

export default async function BlogDetailPage({ params }: { params: Promise<{ slug: string }> }) {
  const { slug } = await params
  const post = blogPosts[slug]

  if (!post) {
    return (
      <div className="min-h-screen">
        <Navbar />
        <div className="mx-auto max-w-7xl px-4 py-20 text-center lg:px-8">
          <h1 className="text-2xl font-bold text-foreground">Post Not Found</h1>
          <Link href="/blog" className="mt-4 inline-block text-primary hover:underline">Back to Blog</Link>
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
            <Link href="/blog" className="hover:text-primary">Blog</Link>
            <span>/</span>
            <span className="text-foreground">{post.title}</span>
          </div>
        </div>
      </section>

      <div className="mx-auto max-w-7xl px-4 py-10 lg:px-8">
        <div className="flex flex-col gap-10 lg:flex-row">
          {/* Main Content */}
          <article className="flex-1">
            <img src={post.image || "/placeholder.svg"} alt={post.title} className="mb-8 h-64 w-full rounded-xl object-cover sm:h-96" />

            <div className="mb-6 flex items-center gap-4 text-sm text-muted-foreground">
              <span className="flex items-center gap-1.5">
                <User className="h-4 w-4" />
                {post.author}
              </span>
              <span className="flex items-center gap-1.5">
                <Calendar className="h-4 w-4" />
                {post.date}
              </span>
            </div>

            <h1 className="mb-6 text-3xl font-bold text-foreground">{post.title}</h1>

            <div className="mb-10 flex flex-col gap-4">
              {post.content.map((paragraph, i) => (
                <p key={i} className="leading-relaxed text-muted-foreground">
                  {paragraph}
                </p>
              ))}
            </div>

            {/* Comments Section */}
            <div className="border-t border-border pt-8">
              <h2 className="mb-6 text-xl font-bold text-foreground">Comments :</h2>
              <div className="flex flex-col gap-6">
                {comments.map((comment) => (
                  <div key={comment.name} className="flex gap-4 rounded-xl border border-border bg-card p-5">
                    <img src={comment.avatar || "/placeholder.svg"} alt={comment.name} className="h-12 w-12 shrink-0 rounded-full object-cover" />
                    <div className="flex-1">
                      <div className="mb-1 flex items-center justify-between">
                        <h4 className="text-sm font-semibold text-foreground">{comment.name}</h4>
                        <button className="flex items-center gap-1 text-xs text-muted-foreground transition-colors hover:text-primary">
                          <Reply className="h-3.5 w-3.5" />
                          Reply
                        </button>
                      </div>
                      <span className="mb-2 block text-xs text-muted-foreground">{comment.date}</span>
                      <p className="text-sm leading-relaxed text-muted-foreground">{comment.text}</p>
                    </div>
                  </div>
                ))}
              </div>

              {/* Comment Form */}
              <div className="mt-8">
                <h3 className="mb-4 text-lg font-semibold text-foreground">Leave a Comment</h3>
                <form className="flex flex-col gap-4">
                  <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                    <input type="text" placeholder="Name*" className="rounded-lg border border-border bg-background px-4 py-2.5 text-sm text-foreground placeholder:text-muted-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary" />
                    <input type="email" placeholder="Email*" className="rounded-lg border border-border bg-background px-4 py-2.5 text-sm text-foreground placeholder:text-muted-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary" />
                  </div>
                  <textarea placeholder="Your Comment*" rows={5} className="rounded-lg border border-border bg-background px-4 py-2.5 text-sm text-foreground placeholder:text-muted-foreground focus:border-primary focus:outline-none focus:ring-1 focus:ring-primary" />
                  <button type="submit" className="self-start rounded-lg bg-primary px-6 py-2.5 text-sm font-medium text-primary-foreground transition-opacity hover:opacity-90">
                    Post Comment
                  </button>
                </form>
              </div>
            </div>
          </article>

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
                {recentPosts.map((rp) => (
                  <Link key={rp.id} href={`/blog/${rp.id}`} className="group flex items-center gap-3">
                    <img src={rp.image || "/placeholder.svg"} alt={rp.title} className="h-16 w-16 shrink-0 rounded-lg object-cover" />
                    <div>
                      <h4 className="text-sm font-medium text-foreground transition-colors group-hover:text-primary">{rp.title}</h4>
                      <span className="text-xs text-muted-foreground">{rp.date}</span>
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
