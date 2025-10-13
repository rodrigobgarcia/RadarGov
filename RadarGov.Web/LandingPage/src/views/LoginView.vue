<template>
    <div class="flex h-screen">
        <div class="flex flex-col justify-center w-1/2 bg-gradient-to-r from-teal-900 to-emerald-950 ">
            <h1 class="text-5xl font-bold text-white p-12 text-center">RadarGov</h1>
        </div>

        <div class="flex items-center items-center justify-center w-1/2 p-24 bg-gray-50">
            <div class="w-full px-24">
                <h2 class="text-5xl font-bold text-gray-800 mb-8">Entrar</h2>
                <form action="" class="space-y-6">
                    <div class="relative">
                        <span class="absolute inset-y-0 left-3 flex items-center text-gray-400">
                            <i class="fa-regular fa-user"></i>
                        </span>
                        <input type="text" placeholder="Usuario ou email" class="w-full pl-12 pr-4 py-4 bg-white border border-gray-200 rounded-xl">
                    </div>
                    <div class="relative">
                        <span class="absolute inset-y-0 left-3 flex items-center text-gray-400">
                            <i class="fa-solid fa-lock"></i>
                        </span>
                        <input type="text" placeholder="Senha" class="w-full pl-12 pr-4 py-4 bg-white border border-gray-200 rounded-xl">
                    </div> 

                    <div class="flex items-center justify-between text-sm">
                        <label for="" class="flex items-center cursor-pointer">
                            <input type="checkbox" class="h-4 w-4 border-gray-300">
                            <span class="ml-2 text-gray-600">Lembrar de mim</span>
                        </label>

                        <a href="">Esqueceu a senha?</a>
                    </div>

                    <button class="text-xl w-full bg-emerald-800 hover:bg-emerald-900 text-white font-semibold py-3 rounded-xl transition-colors shadow-lg shadow-emerald-500/20">
                        Entrar
                    </button>
                    <p class="text-center text-sm text-gray-600">Novo por aqui? <a href="#" class="text-medium text-emerald-600 hover:text-emerald-500">Crie uma conta</a></p>
                </form>
            </div>
        </div>
    </div>
</template>

<script>

</script>