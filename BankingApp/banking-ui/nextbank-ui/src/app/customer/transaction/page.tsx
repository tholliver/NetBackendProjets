import { auth } from '@/auth'
import { TransactionForm } from '@/components/component/transaction-form'

const TransactionPage = async () => {
  const session = await auth()

  return (
    <div>
      <TransactionForm customerId={session?.user.customerId!} />
    </div>
  )
}

export default TransactionPage
