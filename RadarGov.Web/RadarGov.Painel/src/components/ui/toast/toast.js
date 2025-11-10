import { cva } from "class-variance-authority"

export const toastVariants = cva(
  "group pointer-events-auto relative flex w-full items-center justify-between space-x-4 overflow-hidden rounded-md border p-6 pr-8 shadow-lg transition-all",
  {
    variants: {
      variant: {
        default: "bg-white text-neutral-900 border-neutral-200",
        destructive: "bg-red-50 text-red-900 border-red-200",
        success: "bg-green-50 text-green-900 border-green-200",
      },
    },
    defaultVariants: {
      variant: "default",
    },
  }
)
