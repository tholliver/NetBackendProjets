import Image from 'next/image'
import { LandingView } from '@/components/component/landing-view'
import { Sidebar } from '@/components/component/sidebar'

export default function Home() {
  return (
    <main className="">
      <div className="">
        <LandingView />
      </div>
    </main>
  )
}
