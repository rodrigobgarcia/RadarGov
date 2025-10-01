<template>
    <section id="integracoes">
        <div class="flex flex-col items-center w-full py-16 bg-[#253b7f] font-sans">
            <div class="pb-12 text-center">
                <h2 class="text-3xl font-extrabold text-white mb-3 md:text-4xl mb-5">
                    As melhores integrações para o seu negócio
                </h2>
                <p class="text-base font-medium text-white max-w-3xl md:text-lg">
                    O poder das integrações para elevar seu negócio a novos patamares.
                </p>
            </div>
            <LogoMarquee>
                <div class="logo-item">
                    <img src="../../assets/integracoes/gmail.png" alt="Logo Gmail">
                </div>
                <div class="logo-item">
                    <img src="../../assets/integracoes/gov.webp" alt="Logo Gov">
                </div>
                <div class="logo-item">
                    <img src="../../assets/integracoes/gemini.png" alt="Logo Gemini">
                </div>
                <div class="logo-item">
                    <img src="../../assets/integracoes/portal.png" alt="Logo Portal Transparência">
                </div>
                <div class="logo-item">
                    <img src="../../assets/integracoes/chart-js-logo.png" alt="Logo Chart.js">
                </div>
            </LogoMarquee>
        </div>
    </section>
</template>

<script setup>
import LogoMarquee from '../LogoMarquee.vue';
</script>