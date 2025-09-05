<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Ingredient } from '../models/Ingredient.js';

defineProps({
  ingredient : Ingredient,
})
</script>

<template>
  <div>
    <span>{{ ingredient?.name }}</span>
  </div>
</template>

<style scoped lang="scss">

</style>
