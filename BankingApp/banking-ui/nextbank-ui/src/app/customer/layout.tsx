import { Sidebar } from '@/components/component/sidebar'
import { Button } from '@/components/ui/button'
import { auth } from '@/auth'

export default async function Layout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <div className=" bg-muted">
      <Sidebar />
      {/* <header className="bg-primary text-primary-foreground py-4 px-6">
        <div className="flex items-center justify-between">
          <div className="space-y-1">
            <h1 className="text-2xl font-bold">{customerFullName}</h1>
            <p className="text-lg">$5,432.67</p>
          </div>
        </div>
      </header> */}

      <div className="flex-1  md:p-8 lg:p-10">
        <div className="">{children}</div>
      </div>
    </div>
  )
}
