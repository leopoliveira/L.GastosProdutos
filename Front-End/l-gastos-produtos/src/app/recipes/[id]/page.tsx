"use client";

import RecipeForm from "@/app/components/recipe/recipe-form";
import IReadRecipe from "@/common/interfaces/recipe/IReadRecipe";
import RecipeService from "@/common/services/recipe";
import { Box, Spinner, useToast } from "@chakra-ui/react";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";

export default function FormRecipe() {
  const { id } = useParams();
  const [recipe, setRecipe] = useState<IReadRecipe>();
  const [isLoading, setIsLoading] = useState(false);
  const toast = useToast();

  const getData = async (id: string) => {
    setIsLoading(true);
    const response = await RecipeService.GetRecipeById(id);

    setRecipe(response);
    setIsLoading(false);
  };

  useEffect(() => {
    if (id) {
      getData(id as string);
    }
  }, []);

  const formSubmitCallback = (): void => {
    toast({
      title: `Receita salva com sucesso!`,
      status: "success",
      duration: 5000,
      isClosable: true,
    });
  };

  return (
    <main>
      {isLoading ? (
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          height="100vh">
          <Spinner size="xl" />
        </Box>
      ) : (
        <RecipeForm
          recipe={recipe ?? null}
          onFormSubmit={formSubmitCallback}
        />
      )}
    </main>
  );
}
