<template>
    <div class="flex justify-center items-center w-full h-[600px] flex flex-col">

        <div class="flex flex-col items-center text-center gap-4 mb-8">

            <div class="inline-block px-4 py-1 rounded-full font-semibold text-white
            bg-gradient-to-r from-[#DAA520] via-[#FFD700] to-[#DAA520] text-base">
                Inteligência potencializada por IA
            </div>


            <div class="max-w-4xl px-6 text-6xl font-extrabold text-gray-700">
                Descubra oportunidades de Licitações com
                <div>

                    <span class="font-extrabold text-[#253b7f]">Precisão Inteligente</span>
                </div>
            </div>

            <div class="max-w-4xl text-xl font-medium">
                Transforme a sua busca por Licitações com a nossa plataforma que identifica,
                busca e sugere oportunidades de Licitação relevantes para o seu negócio
            </div>

        </div>
        <div class="flex flex-row gap-4">
            <BaseButton>
                 Comece seu Teste Gratuito
            </BaseButton>
            <BaseButton variant="outline">
                Conheça os planos
            </BaseButton>

        </div>
    </div>
    <div class="custom-shape-divider-bottom-1759069318">
    <svg data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1200 120" preserveAspectRatio="none">
        <path d="M321.39,56.44c58-10.79,114.16-30.13,172-41.86,82.39-16.72,168.19-17.73,250.45-.39C823.78,31,906.67,72,985.66,92.83c70.05,18.48,146.53,26.09,214.34,3V0H0V27.35A600.21,600.21,0,0,0,321.39,56.44Z" class="shape-fill"></path>
    </svg>
</div>
</template>

<script setup>
import BaseButton from '../Button.vue'
</script>

<style>
.custom-shape-divider-bottom-1759069318 {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    overflow: hidden;
    line-height: 0;
    transform: rotate(180deg);
}

.custom-shape-divider-bottom-1759069318 svg {
    position: relative;
    display: block;
    width: calc(111% + 1.3px);
    height: 138px;
}

.custom-shape-divider-bottom-1759069318 .shape-fill {
    fill: #F3F6FC;
}
</style>