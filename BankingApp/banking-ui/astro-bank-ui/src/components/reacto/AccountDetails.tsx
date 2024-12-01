'use client'

import React from 'react'
import { useQuery } from '@tanstack/react-query'
import {
  fetchCustomerAccountDetails,
  fetchCustomerAccounts,
} from '@/services/customer'

interface AccoutDetailsProps {
  customerId: string
  accountId: string
}

const AccoutDetails = (props: AccoutDetailsProps) => {
  const tokenMine = 'ddd'
  console.log('Here on params', props.accountId, props.customerId)

  const { isPending, error, data } = useQuery({
    queryKey: ['accountData', tokenMine, props.customerId, props.accountId],
    queryFn: async () =>
      await fetchCustomerAccountDetails(
        tokenMine,
        props.customerId,
        props.accountId
      ),
  })

  if (isPending) return 'Loading...'

  if (error) return 'An error has occurred: ' + error.message

  return <div>{JSON.stringify(data)}</div>
}

export default AccoutDetails
