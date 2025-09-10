<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Ingredient } from '../models/Ingredient.js';
import { Account } from '@/models/Account.js';
import { ingredientService } from '@/services/IngredientService.js';
import { Pop } from '@/utils/Pop.js';

defineProps({
  ingredient: Ingredient,
})

async function deleteIngredient(id) {
  try {
    await ingredientService.deleteIngredient(id);
  } catch (e) {
    Pop.toast(e, 'error');
  }
}

</script>

<template>
  <div class="d-flex">
    <button class="btn" @click="deleteIngredient(ingredient?.id)"><i class="mdi mdi-close text-danger"></i></button>
    <span>{{ ingredient?.quantity }} {{ ingredient?.name }}</span>
  </div>
</template>

<style scoped lang="scss"></style>
