<script setup>
import { Recipe } from '../models/Recipe';

defineProps({
  recipe: Recipe,
})

</script>

<template>
  <div class="position-relative" style="width: 15rem; height: 15rem;">
    <img class="img-fluid object-fit-cover w-100 h-100" :src="recipe?.img" data-bs-toggle="modal"
      data-bs-target="#recipeDetailsModal">
    <span class="text-white position-absolute rounded-4 px-3 py-1" style="background: rgba(0, 0, 0, 0.6)">{{
      recipe?.category }}</span>
    <span class="text-white position-absolute rounded-4 px-3 py-1" style="background: rgba(0, 0, 0, 0.6);">{{
      recipe?.title }}</span>
  </div>
</template>

<style lang="scss"></style>