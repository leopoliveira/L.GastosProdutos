import IReadRecipe from "@/common/interfaces/recipe/IReadRecipe";
import {
  VStack,
  HStack,
  Divider,
  Wrap,
  WrapItem,
  Box,
  Text,
} from "@chakra-ui/react";

type RecipeViewProps = {
  recipe: IReadRecipe;
};

const RecipeVisualizer = ({ recipe }: RecipeViewProps) => {
  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat("pt-BR", {
      style: "currency",
      currency: "BRL",
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    }).format(value);
  };

  return (
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
                    Custo:{" "}
                    {formatCurrency(ingredient.productUnitPrice)}
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
                  <Text fontWeight="bold">{packing.packingName}</Text>
                  <Text>Custo: {formatCurrency(packing.cost)}</Text>
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
  );
};

export default RecipeVisualizer;
