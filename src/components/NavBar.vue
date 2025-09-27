<template>

    <div class="flex justify-between items-center w-full fixed top-0 left-0 border border-black bg-[#f1fcfc]/50 font-extrabold h-32 text-xl font-sans">
        <div class="ml-5 flex items-center gap-2">
            <div><img src="../assets/radargov-high-resolution-logo.png" alt="" class="size-25"></div>
            <div class="text-cyan-900 text-2xl">RADARGOV</div>
        </div>
        <div class="flex gap-8 font-extrabold text-gray-600 text-2xl">
            <div>Dashboard</div>
            <div>
                <router-link to="/oportunidades" class="cursor-pointer hover:text-gray-300">
                    Oportunidades
                </router-link>
            </div>
            <div>Analytics</div>
            <div>Opções</div>
        </div>
        <div class="flex gap-8 w-55 mr-5 items-center">
            <div class="font-bold">
                <span class="material-symbols-outlined">notifications</span>
            </div>
            <div class="bg-cyan-900 rounded-lg px-3 py-2 text-white font-medium flex items-center justify-center ">
                <div>Get Started</div> 
            </div>
        </div>
    </div>
    
</template>