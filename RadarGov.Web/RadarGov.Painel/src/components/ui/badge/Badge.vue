<script setup>
import { cn } from "@/lib/utils";
import { badgeVariants } from ".";

const props = defineProps();
</script>

<template>
  <div :class="cn(badgeVariants({ variant }), props.class)">
    <slot />
  </div>
</template>
