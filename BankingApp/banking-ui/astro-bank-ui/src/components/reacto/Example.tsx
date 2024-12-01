import { useStore } from '@nanostores/react'
import { useQuery } from '@tanstack/react-query'
import { queryClient } from '@/stores/query'

export default function Example() {
  const client = useStore(queryClient)

  const { data, isLoading, error } = useQuery(
    {
      queryKey: ['example'],
      queryFn: async () =>
        await fetch('https://jsonplaceholder.typicode.com/todos/1').then((r) =>
          r.json()
        ),
    },
    client
  )

  if (isLoading)
    return (
      <div>
        <>Is loading</>
      </div>
    )

  if (error)
    return (
      <div>
        <>Some error </>
      </div>
    )
  return (
    <div>
      <h1>{JSON.stringify(data)}</h1>
    </div>
  )
}
