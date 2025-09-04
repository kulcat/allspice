<script setup>
import { Recipe } from '@/models/Recipe';
import { recipeService } from '@/services/RecipeService';

defineProps({
  recipe: Recipe,
}
);

</script>

<template>
  <div class="modal fade" id="recipeDetailsModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-fullscreen">
      <div class="modal-content">
        <div class="modal-body container-fluid d-flex">
          <div class="col-6">
            <img :src="recipe.img">
          </div>
          <div class="col-6">
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Save changes</button>
          </div>
          Details

        </div>
      </div>
    </div>
  </div>
</template>

<style></style>