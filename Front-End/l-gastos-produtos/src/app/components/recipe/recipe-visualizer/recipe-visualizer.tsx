import IReadRecipe from "@/common/interfaces/recipe/IReadRecipe";
import { formatCurrency } from "@/common/services/utils/utils";
import {
  VStack,
  HStack,
  Divider,
  Wrap,
  WrapItem,
  Box,
  Text,
  Button,
} from "@chakra-ui/react";
import { useRouter } from "next/navigation";

type RecipeViewProps = {
  recipe: IReadRecipe;
};

const RecipeVisualizer = ({ recipe }: RecipeViewProps) => {
  const router = useRouter();

  const handleBackClick = () => {
    router.push("/recipes");
  };

  return (
    <>
      <Box
        maxW="1024px"
        mx="auto"
        mt="20px"
        p={4}
        borderWidth={1}
        borderRadius="lg"
        boxShadow="lg">
        <Text
          as="h1"
          fontSize="2xl"
          mb={4}
          textAlign="center">
          Receita
        </Text>
        <VStack
          spacing={4}
          align="start">
          <HStack>
            <Text fontWeight="bold">Nome:</Text>
            <Text>{recipe.name}</Text>
          </HStack>
          {recipe.description && (
            <HStack>
              <Text fontWeight="bold">Descrição:</Text>
              <Text>{recipe.description}</Text>
            </HStack>
          )}
          <Divider />
          <Box width="100%">
            <Text
              fontWeight="bold"
              mb={2}>
              Ingredientes:
            </Text>
            <Wrap spacing="15px">
              {recipe.ingredients.map((ingredient, index) => (
                <WrapItem
                  key={index}
                  minWidth="200px"
                  maxWidth="500px">
                  <Box
                    p={4}
                    borderWidth={1}
                    borderRadius="lg"
                    boxShadow="md"
                    width="100%"
                    bg="green.100"
                    color="green.600">
                    <Text fontWeight="bold">
                      {ingredient.productName}
                    </Text>
                    <Text>Quantidade: {ingredient.quantity}</Text>
                    <Text>
                      Preço:{" "}
                      {formatCurrency(ingredient.ingredientPrice)}
                    </Text>
                    <Text as="strong">
                      Custo:{" "}
                      {formatCurrency(
                        ingredient.ingredientPrice *
                          ingredient.quantity
                      )}
                    </Text>
                  </Box>
                </WrapItem>
              ))}
            </Wrap>
          </Box>
          <Divider />
          <Box width="100%">
            <Text
              fontWeight="bold"
              mb={2}>
              Embalagens:
            </Text>
            <Wrap spacing="15px">
              {recipe.packings.map((packing, index) => (
                <WrapItem
                  key={index}
                  minWidth="200px"
                  maxWidth="500px">
                  <Box
                    p={4}
                    borderWidth={1}
                    borderRadius="lg"
                    boxShadow="md"
                    width="100%"
                    bg="green.100"
                    color="green.600">
                    <Text fontWeight="bold">
                      {packing.packingName}
                    </Text>
                    <Text>Quantidade: {packing.quantity}</Text>
                    <Text>
                      Preço:{" "}
                      {formatCurrency(packing.packingUnitPrice)}
                    </Text>
                    <Text as="strong">
                      Custo:{" "}
                      {formatCurrency(
                        packing.packingUnitPrice * packing.quantity
                      )}
                    </Text>
                  </Box>
                </WrapItem>
              ))}
            </Wrap>
          </Box>
          <Divider />
          <HStack>
            <Text
              fontWeight="bold"
              fontSize="x-large">
              Custo Total:
            </Text>
            <Text fontSize="x-large">
              {formatCurrency(recipe.totalCost)}
            </Text>
          </HStack>
        </VStack>
      </Box>
      <Box
        maxW="1024px"
        mx="auto"
        mt="20px"
        textAlign={"right"}
        p={4}>
        <Button
          width="150px"
          colorScheme="blue"
          onClick={handleBackClick}>
          Voltar
        </Button>
      </Box>
    </>
  );
};

export default RecipeVisualizer;
