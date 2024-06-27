"use client";

import { useEffect, useState } from "react";
import { Spinner, Box } from "@chakra-ui/react";
import IReadRecipe from "@/common/interfaces/recipe/IReadRecipe";
import RecipeService from "@/common/services/recipe";
import RecipeGrid from "../components/recipe/recipe-data-grid/recipe-data-grid";

export default function Recipes() {
  const [recipes, setRecipes] = useState<IReadRecipe[]>([]);
  const [loading, setLoading] = useState(true);
  const [reRender, setReRender] = useState(false);

  const getData = async () => {
    setLoading(true);

    const response = await RecipeService.GetAllRecipes();

    setRecipes(response);
    setLoading(false);
  };

  useEffect(() => {
    getData();
  }, []);

  useEffect(() => {
    if (!reRender) {
      return;
    }
    getData();
    setReRender(false);
  }, [reRender]);

  return (
    <main>
      {loading ? (
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          height="100vh">
          <Spinner size="xl" />
        </Box>
      ) : (
        <RecipeGrid
          recipes={recipes}
          onSubmit={() => setReRender(true)}
        />
      )}
    </main>
  );
}
