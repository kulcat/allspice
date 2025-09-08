<script setup>
import { Recipe } from '../models/Recipe';

defineProps({
  recipe: Recipe,
})

const imgError = (e) => {
  e.target.src = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
}

function capitalize(str) {
  return str ? str.charAt(0).toUpperCase() + str.slice(1) : "";
}

</script>

<template>
  <div class="d-flex position-relative" style="width: 22.5rem; height: 22.5rem;">
    <img class="img-fluid object-fit-cover w-100 h-100" :src="recipe?.img" @error="imgError" data-bs-toggle="modal"
      data-bs-target="#viewRecipeModal">

    <div class="d-flex justify-content-left position-absolute top-0 start-0 w-100 p-2">
      <span class="text-white rounded-4 px-3 py-1 fs-5" style="background: rgba(0, 0, 0, 0.6)">
        {{ capitalize(recipe?.category) }}
      </span>
    </div>

    <div class="d-flex justify-content-center position-absolute bottom-0 start-0 w-100 p-2">
      <span class="text-white rounded-4 px-3 py-1 w-100 fs-3" style="background: rgba(0, 0, 0, 0.6);">
        {{ capitalize(recipe?.title) }}
      </span>
    </div>

    <!-- favorite -->
  </div>
</template>

<style lang="scss"></style>