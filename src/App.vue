<script setup>
import { RouterLink, RouterView } from 'vue-router'
import NavBar from './components/NavBar.vue';


</script>

<template>
<NavBar></NavBar>

<main class="pt-16"> 
    <RouterView />
</main>
</template>

<style scoped>

</style>
