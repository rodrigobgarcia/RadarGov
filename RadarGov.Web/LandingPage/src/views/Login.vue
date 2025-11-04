<template>
    <div class="relative">
        <div class="absolute inset-0 min-h-screen  w-full flex items-center justify-center  bg-cover bg-center bg-no-repeat"
            :style="{ backgroundImage: 'url(/RadarGov/martelo3.jpg)' }">
            <div class="absolute inset-0 opacity-30 bg-gradient-to-br from-gray-900 via-[#002200] to-gray-900"></div>
            <div class="w-full max-w-md backdrop-blur-xl bg-white/10 rounded-3xl p-8 shadow-2xl border border-white/20">
                <div class="flex justify-center mb-8">
                    <div
                        class="h-24 w-24 rounded-full bg-gradient-to-br from-green-800/40 to-green/900/40 backdrop-blur-sm flex items-center justify-center border border-white/20">
                        <CircleUserRound :size="40" class="text-white/80"></CircleUserRound>
                    </div>
                </div>
                <form action="" class="space-y-6">
                    <div class="relative">
                        <div class="absolute left-0 top-1/2 -translate-y-1/2 text-white/60">
                            <CircleUserRound :size="20" />
                        </div>
                        <input type="text" placeholder="Usuário"
                            class="w-full bg-transparent border-b border-white/30 text-white placeholder-white/50 py-3 pl-8 pr-2 focus:outline-none focus:border-green-400 transition-colors">
                    </div>

                    <div class="relative">
                        <div class="absolute left-0 top-1/2 -translate-y-1/2 text-white/60">
                            <lock :size="20"></lock>
                        </div>
                        <input type="password" placeholder="Senha"
                            class="w-full bg-transparent border-b border-white/30 text-white placeholder-white/50 py-3 pl-8 pr-2 focus:outline-none focus:border-green-400 transition-colors">
                    </div>

                    <button type="submit"
                        class="w-full bg-gradient-to-r from-green-800 to-green-600 hover:from-green-700 hover:to-green-500 text-white font-semibold py-4 rounded-full transition-all duration-300 shadow-lg hover:shadow-green-500/10 mt-5">
                        ENTRAR
                    </button>

                    <p class="text-center text-white/60 text-sm mt-3">Esqueceu a senha?
                        <button class="text-green-300 hover:text-green-200 transition-colors ml-1">
                            Recuperar senha
                        </button>
                    </p>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { CircleUserRound, FileText, Mail, Lock } from 'lucide-vue-next';
</script>