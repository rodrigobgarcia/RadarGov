<template>
    <section id="planos">
        <div class="flex flex-col items-center w-full py-16 bg-[#F3F6FC] font-sans pb-32">
            <div class="text-center mb-10">
                <h2 class="text-3xl font-extrabold text-gray-700 mb-3 md:text-4xl">
                    Encontre o Plano Certo para o Seu Negócio
                </h2>
                <p class="text-base font-medium text-gray-600 max-w-3xl md:text-lg">
                    Maximize sua precisão e assertividade na busca por Licitações com o plano ideal para as suas
                    necessidades.
                </p>
            </div>

            <div class="flex flex-col md:flex-row gap-6 px-4 max-w-6xl w-full">

                <div
                    class="flex-1 bg-white rounded-xl p-6 flex flex-col border border-gray-200 transition-all duration-300 hover:border-[#c2dcff]">
                    <h3 class="text-xl font-extrabold text-gray-700 mb-2 md:text-2xl">Free</h3>
                    <p class="text-sm text-gray-500 mb-4 md:text-base">Comece a monitorar licitações gratuitamente.</p>

                    <div class="mb-6">
                        <div class="flex items-center justify-start gap-3 mb-1">
                            <span class="text-lg font-medium text-gray-400 line-through">De R$ 59,99</span>
                            <div
                                class="inline-block px-2 py-1 rounded-full text-xs font-bold text-[#253b7f] bg-[#c2dcff]">
                                ECONOMIZE R$ 59,99
                            </div>
                        </div>

                        <div class="flex items-baseline gap-1">
                            <span class="text-base font-medium text-gray-600">Por R$</span>
                            <span class="text-4xl font-extrabold text-[#253b7f] leading-none">0,00</span>
                            <span class="text-base font-medium text-gray-500">/ Mês</span>
                        </div>
                    </div>

                    <ul class="space-y-3 text-gray-700 flex-grow mb-6">
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-medium">1 Categoria de Monitoramento</span>
                        </li>
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-medium">Alertas Básicos por E-mail</span>
                        </li>
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-medium">Histórico Limitado de Dados</span>
                        </li>
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-gray-400 text-lg">remove_circle</span>
                            <span class="text-gray-400">Sem Alertas em Tempo Real</span>
                        </li>
                    </ul>

                    <BaseButton variant="outline" class="w-full text-sm py-2">
                        Comece Grátis
                    </BaseButton>
                </div>

                <div
                    class="flex-1 bg-white rounded-xl p-6 flex flex-col border border-gray-200 transition-all duration-300 hover:border-[#c2dcff]">
                    <h3 class="text-xl font-extrabold text-gray-700 mb-2 md:text-2xl">Pro</h3>
                    <p class="text-sm text-gray-500 mb-4 md:text-base">Para empresas que buscam mais assertividade.</p>

                    <div class="mb-6">
                        <div class="flex items-center justify-start gap-3 mb-1">
                            <span class="text-lg font-medium text-gray-400 line-through">De R$ 199,99</span>
                            <div
                                class="inline-block px-2 py-1 rounded-full text-xs font-bold text-[#253b7f] bg-[#c2dcff]">
                                ECONOMIZE R$ 100,00
                            </div>
                        </div>

                        <div class="flex items-baseline gap-1">
                            <span class="text-base font-medium text-gray-600">Por R$</span>
                            <span class="text-4xl font-extrabold text-[#253b7f] leading-none">99,99</span>
                            <span class="text-base font-medium text-gray-500">/ Mês</span>
                        </div>
                    </div>

                    <ul class="space-y-3 text-gray-700 flex-grow mb-6">
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-medium">Até 5 Categorias de Monitoramento</span>
                        </li>
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-medium">Alertas em Tempo Real</span>
                        </li>
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-medium">Histórico Completo de Licitações</span>
                        </li>
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-gray-400 text-lg">remove_circle</span>
                            <span class="text-gray-400">Sem Relatórios Avançados</span>
                        </li>
                    </ul>

                    <BaseButton class="w-full text-sm py-2">
                        Contratar Plano Pro
                    </BaseButton>
                </div>
                <div
                    class="teste flex-1 bg-white rounded-xl p-6 flex flex-col border border-gray-200 transition-all duration-300 hover:border-[#FFD700]">

                    <div class="reco inline-block px-2 py-0.5 rounded-full font-semibold text-white text-sm self-start mb-3
    bg-gradient-to-r from-[#DAA520] via-[#FFD700] to-[#DAA520] text-center">
                        Mais Recomendado
                    </div>
                    <!-- <div class="flex-1 bg-white rounded-xl p-6 flex flex-col border border-gray-200 transition-all duration-300 hover:border-[#FFD700]"> -->


                    <h3 class="text-xl font-extrabold text-[#253b7f] mb-2 md:text-2xl">Premium</h3>
                    <p class="text-sm text-gray-500 mb-4 md:text-base">A solução completa com Inteligência Artificial.
                    </p>

                    <div class="mb-6">
                        <div class="flex items-center justify-start gap-3 mb-1">
                            <span class="text-lg font-medium text-gray-400 line-through">De R$ 259,00</span>
                            <div
                                class="inline-block px-2 py-1 rounded-full text-xs font-bold text-[#253b7f] bg-[#c2dcff]">
                                ECONOMIZE R$ 60,00
                            </div>
                        </div>

                        <div class="flex items-baseline gap-1">
                            <span class="text-base font-medium text-gray-600">Por R$</span>
                            <span class="text-4xl font-extrabold text-[#253b7f] leading-none">199,00</span>
                            <span class="text-base font-medium text-gray-500">/ Mês</span>
                        </div>
                    </div>

                    <ul class="space-y-3 text-gray-700 flex-grow mb-6">
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-extrabold text-[#253b7f]">Categorias Ilimitadas</span>
                        </li>
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-medium">Alertas em Tempo Real e Notificação por E-mail</span>
                        </li>
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-medium">Relatórios Avançados (PDF) e Histórico Completo</span>
                        </li>
                        <li class="flex items-center gap-2 text-sm md:text-base">
                            <span class="material-symbols-outlined text-green-500 text-lg">check_circle</span>
                            <span class="font-medium">Acesso ao Chatbot de Editais (IA)</span>
                        </li>
                    </ul>

                    <BaseButton>
                        Contratar Plano Premium
                    </BaseButton>
                    <!-- </div> -->
                </div>
            </div>
        </div>
    </section>
</template>

<script setup>
import BaseButton from '../Button.vue' 
</script>

<style>
.teste {
    position: relative;
    /* top: -40px; */
    z-index: 10;
}

.reco {
    position: absolute;
    top: -10px;
    right: 20px;
    z-index: 20;
}
</style>