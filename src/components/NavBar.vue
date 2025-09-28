<template>
    <div class="flex justify-between items-center w-full fixed top-0 left-0 h-16 text-xl font-sans px-[100px] transition-colors duration-300 z-1000"
        :class="isScrolled ? 'bg-[#f3f6fc]/95 shadow-md' : 'bg-transparent'">
        <div class="flex items-center gap-2">
            <img src="../assets/logo.png" alt="Logo RadarGov" class="h-10" />
        </div>

        <div class="flex gap-8 text-gray-600 text-xl">
            <div>Dashboard</div>
            <div>
                <router-link to="/oportunidades" class="cursor-pointer hover:text-gray-300">
                    Oportunidades
                </router-link>
            </div>
            <div>Analytics</div>
            <div>Opções</div>
        </div>

        <div class="flex gap-8 items-center">
            <!-- <div class="cursor-pointer hover:text-gray-300">
                <span class="material-symbols-outlined">notifications</span>
            </div> -->
            <BaseButton>
                Comece já!
            </BaseButton>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount } from 'vue'
import BaseButton from './Button.vue'
const isScrolled = ref(false)

function handleScroll() {
    isScrolled.value = window.scrollY > 0
}

onMounted(() => {
    window.addEventListener('scroll', handleScroll)
})
onBeforeUnmount(() => {
    window.removeEventListener('scroll', handleScroll)
})
</script>
