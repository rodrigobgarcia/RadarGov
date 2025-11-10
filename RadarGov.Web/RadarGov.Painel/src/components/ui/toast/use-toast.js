import { ref } from "vue"

const toasts = ref([])

export function useToast() {
  function toast(options) {
    const id = options.id ?? Math.random().toString(36).slice(2, 9)
    const duration = options.duration ?? 3000

    const newToast = { ...options, id, open: true }
    toasts.value.push(newToast)

    // Remove automaticamente após o tempo definido
    setTimeout(() => {
      dismiss(id)
    }, duration)

    return id
  }

  function dismiss(id) {
    const index = toasts.value.findIndex((t) => t.id === id)
    if (index !== -1) {
      toasts.value.splice(index, 1)
    }
  }

  return { toast, dismiss, toasts }
}
