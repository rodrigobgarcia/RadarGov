<template>
    <div class="relative">
        <div class="absolute inset-0 min-h-screen  w-full flex items-center justify-center  bg-cover bg-center bg-no-repeat"
            :style="{ backgroundImage: 'url(/RadarGov/empresa.jpg)' }">
            <div class="absolute inset-0 opacity-40 bg-gradient-to-br from-gray-900 via-green-950 to-gray-900"></div>
            <div class="w-full max-w-md backdrop-blur-xl bg-white/10 rounded-3xl p-8 shadow-2xl border border-white/20">
                <div class="flex justify-center mb-8">
                    <div
                        class="h-24 w-24 rounded-full bg-gradient-to-br from-green-800/40 to-green/900/40 backdrop-blur-sm flex items-center justify-center border border-white/20">
                        <building2 :size="40" class="text-white/80"></building2>
                    </div>
                </div>
                <form action="" class="space-y-6">
                    <div class="relative">
                        <div class="absolute left-0 top-1/2 -translate-y-1/2 text-white/60">
                            <Building2 :size="20" />
                        </div>
                        <input type="text" placeholder="Nome da Empresa"
                            class="w-full bg-transparent border-b border-white/30 text-white placeholder-white/50 py-3 pl-8 pr-2 focus:outline-none focus:border-green-400 transition-colors">
                    </div>

                    <div class="relative">
                        <div class="absolute left-0 top-1/2 -translate-y-1/2 text-white/60">
                            <file-text :size="20"></file-text>
                        </div>
                        <input type="text" placeholder="Cnpj"
                            class="w-full bg-transparent border-b border-white/30 text-white placeholder-white/50 py-3 pl-8 pr-2 focus:outline-none focus:border-green-400 transition-colors">
                    </div>

                    <div class="relative">
                        <div class="absolute left-0 top-1/2 -translate-y-1/2 text-white/60">
                            <mail :size="20"></mail>
                        </div>
                        <input type="text" placeholder="Email"
                            class="w-full bg-transparent border-b border-white/30 text-white placeholder-white/50 py-3 pl-8 pr-2 focus:outline-none focus:border-green-400 transition-colors">
                    </div>

                    <div class="relative">
                        <div class="absolute left-0 top-1/2 -translate-y-1/2 text-white/60">
                            <lock :size="20"></lock>
                        </div>
                        <input type="text" placeholder="Senha"
                            class="w-full bg-transparent border-b border-white/30 text-white placeholder-white/50 py-3 pl-8 pr-2 focus:outline-none focus:border-green-400 transition-colors">
                    </div>

                    <button type="submit"
                        class="w-full bg-gradient-to-r from-green-800 to-green-600 hover:from-green-700 hover:to-green-500 text-white font-semibold py-4 rounded-full transition-all duration-300 shadow-lg hover:shadow-green-500/10 mt-5">
                        CADASTRAR
                    </button>

                    <p class="text-center text-white/60 text-sm mt-3">Já tem uma conta?
                        <button class="text-green-300 hover:text-green-200 transition-colors ml-1">
                            Fazer login
                        </button>
                    </p>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { Building2, FileText, Mail, Lock } from 'lucide-vue-next';
</script>