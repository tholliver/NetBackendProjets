import { auth } from '@/auth'
import AccountDetails from '@/components/AccountDetails'

export default async function Customer() {
  const session = await auth()

  const customerFullName = `${session?.user.firstName} ${session?.user.lastName} `

  return (
    <div>
      <AccountDetails
        customerFullName={customerFullName}
        customerId={session?.user.customerId!}
      />
    </div>
  )
}
