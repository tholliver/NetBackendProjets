'use client'

import { fetchCustomerAccountDetails } from '@/services/customers'
import { useQuery } from '@tanstack/react-query'
import React from 'react'

interface AccountTProps {
  customerId: string
  accountId: string
}

const AccountT = (props: AccountTProps) => {
  const token = 'd'
  const { isPending, error, data } = useQuery({
    queryKey: ['accountData', token, props.customerId, props.accountId],
    queryFn: async () =>
      await fetchCustomerAccountDetails(
        token,
        props.customerId,
        props.accountId
      ),
  })

  if (isPending) return 'Loading...'

  if (error) return 'An error has occurred: ' + error.message
  return <div>{JSON.stringify(data)}</div>
}

export default AccountT
