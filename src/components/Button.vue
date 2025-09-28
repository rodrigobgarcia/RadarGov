<template>
  <button
    :class="[
      'inline-flex items-center justify-center font-medium transition-colors duration-200 rounded-lg max-h-[34px] cursor-pointer',
      sizeClasses,
      variantClasses,
      fullWidth ? 'w-full' : ''
    ]"
    @click="$emit('click')"
  >
    <slot />
  </button>
</template>

<script setup>
import { computed } from 'vue'

const props = defineProps({
  /** cor/estilo principal */
  variant: {
    type: String,
    default: 'primary', // primary | secondary | outline
  },
  /** tamanho do botão */
  size: {
    type: String,
    default: 'md', // sm | md | lg
  },
  /** ocupa 100% da largura */
  fullWidth: {
    type: Boolean,
    default: false,
  },
})

/* 🎨 ESTILOS DE VARIANTES */
const variantClasses = computed(() => {
  switch (props.variant) {
    case 'secondary':
      return 'bg-gray-200 text-gray-800 hover:bg-gray-300'
    case 'outline':
      return 'bg-transparent border border-[#253b7f] text-[#253b7f] hover:bg-[#253b7f] hover:text-white'
    default:
      return 'bg-[#253b7f] text-white hover:bg-[#1e3166]'
  }
})

/* 📏 ESTILOS DE TAMANHO */
const sizeClasses = computed(() => {
  switch (props.size) {
    case 'sm':
      return 'px-3 py-1 text-sm'
    case 'lg':
      return 'px-6 py-3 text-lg'
    default:
      return 'px-4 py-2 text-[16px]'
  }
})
</script>
