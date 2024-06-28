"use client";

import RecipeVisualizer from "@/app/components/recipe/recipe-visualizer";
import IReadRecipe from "@/common/interfaces/recipe/IReadRecipe";
import RecipeService from "@/common/services/recipe";
import { Box, Spinner } from "@chakra-ui/react";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";

export default function VisualizeRecipe() {
  const [recipe, setRecipe] = useState<IReadRecipe>({
    id: "",
    name: "",
    description: "",
    totalCost: 0,
    ingredients: [],
    packings: [],
    quantity: 0,
    sellingValue: 0
  });
  const [isLoading, setIsLoading] = useState(true);

  const { id } = useParams();

  const getData = async (recipeId: string) => {
    const response = await RecipeService.GetRecipeById(recipeId);
    setRecipe(response);
  };

  useEffect(() => {
    setIsLoading(true);
    if (id) {
      getData(id as string);
    }

    setIsLoading(false);
  }, []);

  return isLoading ? (
    <Box
      display="flex"
      justifyContent="center"
      alignItems="center"
      height="100vh">
      <Spinner size="xl" />
    </Box>
  ) : (
    <RecipeVisualizer recipe={recipe} />
  );
}
