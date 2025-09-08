<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Ingredient } from '../models/Ingredient.js';
import { Account } from '@/models/Account.js';

defineProps({
  ingredient: Ingredient,
})

</script>

<template>
  <div class="d-flex">
    <span>{{ ingredient?.quantity }} {{ ingredient?.name }}</span>
  </div>
</template>

<style scoped lang="scss"></style>
