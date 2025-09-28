<template>
    <div class="custom-shape-divider-top-1759082264">
        <svg data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1200 120" preserveAspectRatio="none">
            <path
                d="M985.66,92.83C906.67,72,823.78,31,743.84,14.19c-82.26-17.34-168.06-16.33-250.45.39-57.84,11.73-114,31.07-172,41.86A600.21,600.21,0,0,1,0,27.35V120H1200V95.8C1132.19,118.92,1055.71,111.31,985.66,92.83Z"
                class="shape-fill"></path>
        </svg>
    </div>
    <section class="py-16 bg-[#F3F6FC]" id="contato">

        <div class="mt-[140px]">

            <div class="flex flex-col lg:flex-row justify-center items-center gap-[10rem]">

                <div class="hidden lg:block lg:w-1/2 max-w-xs">
                    <img src="../../assets/ilustration.png"
                        alt="Ilustração de Pessoas de Negócios Analisando Dados com IA"
                        class="w-full h-auto object-contain">
                    <div class="text-center mb-8 z-[12] relative py-1">
                        <h2 class="text-3xl font-extrabold text-gray-700 mb-3 md:text-4xl">
                            Agende sua demonstração
                        </h2>
                        <p class="text-md text-gray-600">
                            Descubra como a IA pode impulsionar suas vitórias em licitações.
                        </p>
                    </div>
                </div>

                <form action="#" method="POST"
                    class="w-full lg:w-[450px] bg-white p-8 md:p-10 rounded-2xl shadow-xl space-y-5 border border-gray-100">

                    <div>
                        <label for="nome" class="block text-sm font-semibold text-gray-700 mb-1">Seu Nome</label>
                        <input type="text" name="nome" id="nome" required
                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-[#253b7f] transition duration-200"
                            placeholder="Ex: João Silva">
                    </div>

                    <div>
                        <label for="empresa" class="block text-sm font-semibold text-gray-700 mb-1">Empresa</label>
                        <input type="text" name="empresa" id="empresa" required
                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-[#253b7f] transition duration-200"
                            placeholder="Ex: RadarGov">
                    </div>

                    <div>
                        <label for="email" class="block text-sm font-semibold text-gray-700 mb-1">E-mail
                            Corporativo</label>
                        <input type="email" name="email" id="email" required
                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-[#253b7f] transition duration-200"
                            placeholder="seu@empresa.com.br">
                    </div>

                    <div>
                        <label for="telefone" class="block text-sm font-semibold text-gray-700 mb-1">Telefone /
                            WhatsApp</label>
                        <input type="tel" name="telefone" id="telefone" required
                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-[#253b7f] transition duration-200"
                            placeholder="(XX) XXXXX-XXXX">
                    </div>

                    <div class="pt-3 flex justify-center">
                        <BaseButton>
                            Conhecer a plataforma
                        </BaseButton>
                    </div>

                    <p class="text-xs text-gray-500 text-center pt-1">
                        Seus dados estão seguros. Usaremos eles apenas para agendar sua demonstração.
                    </p>
                </form>
            </div>

        </div>
    </section>
</template>

<script setup>
import BaseButton from '../Button.vue'
</script>

<style>
.contato {
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 15rem;
}

.custom-shape-divider-top-1759082264 {
    position: absolute;
    /* top: 0; */
    /* left: 0; */
    width: 100%;
    overflow: hidden;
    line-height: 0;
    z-index: 10;
    transform: rotate(180deg);
}

.custom-shape-divider-top-1759082264 svg {
    position: relative;
    display: block;
    width: calc(108% + 1.3px);
    height: 107px;
    transform: rotateY(180deg);
}

.custom-shape-divider-top-1759082264 .shape-fill {
    fill: #FFFFFF;
}
</style>