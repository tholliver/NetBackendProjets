
export const fetcher = <T>(url: string): Promise<T[]> =>
  fetch(url).then((r) => r.json())

export const objFetcher = <T>(url: string): Promise<T> =>
  fetch(url).then((r) => r.json())

export const puller = (url: string, body: any) =>
  fetch(url, {
    method: 'POST',
    body: body,
  })
