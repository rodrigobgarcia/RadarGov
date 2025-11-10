<script setup lang="ts">
import { cn } from "@/lib/utils"
import { toastVariants } from "./toast"
import { X } from "lucide-vue-next"
import { ToastClose, ToastDescription, ToastProvider, ToastRoot, ToastTitle, ToastViewport } from "radix-vue"

import type { ToastRootEmits, ToastProps } from "radix-vue"

const props = defineProps<ToastProps>()
const emits = defineEmits<ToastRootEmits>()
</script>

<template>
  <ToastProvider>
    <ToastRoot
      v-bind="props"
      @update:open="emits('update:open', $event)"
      :class="cn(toastVariants({ variant: props.variant }), props.class)"
    >
      <div class="grid gap-1">
        <ToastTitle v-if="props.title" class="font-semibold">
          {{ props.title }}
        </ToastTitle>
        <ToastDescription v-if="props.description" class="text-sm opacity-90">
          {{ props.description }}
        </ToastDescription>
      </div>
      <ToastClose class="absolute right-2 top-2 text-muted-foreground hover:text-foreground">
        <X class="h-4 w-4" />
      </ToastClose>
    </ToastRoot>

    <ToastViewport class="fixed bottom-4 right-4 z-[100] flex flex-col gap-2" />
  </ToastProvider>
</template>
