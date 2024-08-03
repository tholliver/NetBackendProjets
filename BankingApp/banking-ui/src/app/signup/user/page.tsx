import { ReusableForm } from '@/components/forms/ReusableForm'
import MadeForm from '@/components/forms/MadeForm'
import { customerSchema } from '@/schemas'
import { CustomerType } from '@/models'
import Formix from '@/components/forms/Formix'
import { StepForm } from '@/components/component/step-form'
import { SignUpForm } from '@/components/component/SignUpForm'

const UserRegistration = () => {
  return (
    <div>
      {/* <StepForm /> */}
      <SignUpForm />

      {/* <div className="flex min-h-screen items-center justify-centers"></div> */}
    </div>
  )
}

export default UserRegistration
