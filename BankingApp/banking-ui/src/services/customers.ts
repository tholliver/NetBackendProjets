import { fetcher } from "@/utils/mutans";

export async function fetchStudents(pagination: number = 5) {
    return fetcher<Customer>('/api/students/all')
}