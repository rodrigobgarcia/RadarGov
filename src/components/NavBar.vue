<template>

    <div class="flex justify-between items-center w-full fixed top-0 left-0 bg-[#e6eef9]/95 h-16 text-xl font-sans px-[40px]">
        <div class="flex items-center gap-2">
            <!-- <div><img src="../assets/radargov-high-resolution-logo.png" alt="" class="size-25"></div>
            <div class="text-cyan-900 text-2xl">RADARGOV</div> -->
            <img src="../assets/logo.png" alt="Logo RadarGov" class="h-10">
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
        <div class="flex gap-8  items-center">
            <div class="cursor-pointer hover:text-gray-300">
                <span class="material-symbols-outlined">notifications</span>
            </div>
            <button class="bg-[#253b7f] rounded-lg px-3 py-2 text-white text-[16px] flex items-center justify-center max-h-[34px]">
                Get Started
            </button>
        </div>
    </div>
    
</template>