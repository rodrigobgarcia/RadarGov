<script setup>
import { RouterView, useRoute } from 'vue-router'
import SideMenu from './components/SideMenu.vue'

const route = useRoute()
</script>

<template>
    <!-- <NavBar v-if="route.path === '/' && route.path !== '/login'" />-->
    <SideMenu v-if="route.path !== '/' && route.path !== '/login'" /> 

    <RouterView />
    <!-- <main :class="route.path === '/' ? 'pt-16' : route.path !== '/login'?  'ml-[70px]' : ''"> -->
    <!-- </main> -->
</template>

<style scoped>
main {
    min-height: 100vh;
}
</style>
