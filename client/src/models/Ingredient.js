
export class Ingredient {
  constructor(data) {
    this.id = data.id;
    this.name = data.name;
    this.quantity = data.quantity;
    this.recipe_id = data.recipe_id;
  }
}
