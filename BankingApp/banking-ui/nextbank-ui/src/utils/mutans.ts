export const fetcher = <T>(url: string): Promise<T[]> =>
  fetch(url).then((r) => r.json())

export const objFetcher = <T>(url: string): Promise<T> =>
  fetch(url).then((r) => r.json())

export const authPuller = <T>(url: string, token: string): Promise<T> =>
  fetch(url, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json',
    },
  }).then(r => r.json());


export const puller = async <T, R>(url: string, body: T): Promise<R> => {
  const response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(body),
  });

  if (!response.ok) {
    if (response.status === 401) {
      console.log('Unauthorized: Token might be invalid or expired.');
      throw new Error('Unauthorized');
    }
    console.log('Error on response');
    throw new Error('Request failed');
  }

  // console.log('--------------LOG ON PULLER--------', response.json());
  return response.json() as Promise<R>;
};
