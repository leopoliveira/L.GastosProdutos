import IReadRecipe from "@/common/interfaces/recipe/IReadRecipe";
import IngredientDto from "@/common/interfaces/recipe/dtos/IngredientDto";
import PackingDto from "@/common/interfaces/recipe/dtos/PackingDto";
import PackingService from "@/common/services/packing";
import ProductService from "@/common/services/product";
import RecipeService from "@/common/services/recipe";
import {
  useDisclosure,
  FormControl,
  FormLabel,
  Input,
  Button,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  ModalFooter,
  Box,
  Text,
  Tag,
  TagCloseButton,
  TagLabel,
  Wrap,
  WrapItem,
  useToast,
} from "@chakra-ui/react";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

type RecipeFormProps = {
  recipe: IReadRecipe | null;
  onFormSubmit: () => void;
};

const RecipeForm: React.FC<RecipeFormProps> = ({
  recipe,
  onFormSubmit,
}) => {
  const {
    isOpen: isIngredientsOpen,
    onOpen: onIngredientsOpen,
    onClose: onIngredientsClose,
  } = useDisclosure();

  const {
    isOpen: isPackingsOpen,
    onOpen: onPackingsOpen,
    onClose: onPackingsClose,
  } = useDisclosure();

  const [formData, setFormData] = useState<IReadRecipe>({
    id: "",
    name: "",
    description: "",
    totalCost: 0,
    ingredients: [],
    packings: [],
    quantity: 0,
    sellingValue: 0,
  });
  const [searchTerm, setSearchTerm] = useState("");
  const [availableIngredients, setAvailableIngredients] = useState<
    IngredientDto[]
  >([]);
  const [availablePackings, setAvailablePackings] = useState<
    PackingDto[]
  >([]);
  const [newIngredient, setNewIngredient] = useState<
    Partial<IngredientDto>
  >({});
  const [newPacking, setNewPacking] = useState<Partial<PackingDto>>(
    {}
  );
  const [editIndex, setEditIndex] = useState<number | null>(null);
  const router = useRouter();
  const toast = useToast();

  useEffect(() => {
    if (recipe) {
      setFormData(recipe);
    } else {
      setFormData({
        id: "",
        name: "",
        description: "",
        totalCost: 0,
        ingredients: [],
        packings: [],
        quantity: 0,
        sellingValue: 0,
      });
    }
  }, [recipe]);

  const handleRemoveIngredient = (
    ingredient: IngredientDto,
    e: React.MouseEvent
  ) => {
    e.stopPropagation();
    setFormData((prevState) => ({
      ...prevState,
      ingredients: prevState.ingredients.filter(
        (i) => i !== ingredient
      ),
    }));
  };

  const handleRemovePacking = (
    packing: PackingDto,
    e: React.MouseEvent
  ) => {
    e.stopPropagation();
    setFormData((prevState) => ({
      ...prevState,
      packings: prevState.packings.filter((p) => p !== packing),
    }));
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setFormData((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSearchChange = (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSearchTerm(e.target.value);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (formData.name === "" || formData.ingredients.length === 0) {
      toast({
        title: `Preencha todos os campos`,
        status: "error",
        duration: 5000,
        isClosable: true,
      });
      return;
    }

    if (formData.id) {
      RecipeService.UpdateRecipe(formData.id, formData).then(() => {
        router.push("/recipes");
      });
    } else {
      RecipeService.CreateRecipe(formData).then(() => {
        router.push("/recipes");
      });
    }

    onFormSubmit();
  };

  const fetchIngredients = async () => {
    const ingredients = await ProductService.GetAllIngredientsDto();

    setAvailableIngredients(ingredients);
  };

  const fetchPackings = async () => {
    const packings = await PackingService.GetAllPackingsDto();

    setAvailablePackings(packings);
  };

  const handleNewIngredientChange = (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    const { name, value } = e.target;
    setNewIngredient((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleNewPackingChange = (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    const { name, value } = e.target;
    setNewPacking((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleAddIngredient = () => {
    if (!newIngredient.quantity || newIngredient.quantity <= 0) {
      alert("Quantidade deve ser maior que 0.");
      return;
    }

    if (editIndex !== null) {
      const updatedIngredients = [...formData.ingredients];
      updatedIngredients[editIndex] = newIngredient as IngredientDto;

      setFormData((prevState) => ({
        ...prevState,
        ingredients: updatedIngredients,
      }));
    } else {
      const ingredient = availableIngredients.find(
        (i: IngredientDto) => i.productId === newIngredient.productId
      );
      if (ingredient) {
        setFormData((prevState) => ({
          ...prevState,
          ingredients: [
            ...prevState.ingredients,
            {
              ...ingredient,
              quantity: newIngredient.quantity,
            } as IngredientDto,
          ],
        }));
      }
    }
    setNewIngredient({});
    setEditIndex(null);
    onIngredientsClose();
  };

  const handleAddPacking = () => {
    if (!newPacking.quantity || newPacking.quantity <= 0) {
      alert("Quantidade deve ser maior que 0.");
      return;
    }

    if (editIndex !== null) {
      const updatedPackings = [...formData.packings];
      updatedPackings[editIndex] = newPacking as PackingDto;

      setFormData((prevState) => ({
        ...prevState,
        packings: updatedPackings,
      }));
    } else {
      const packing = availablePackings.find(
        (p: PackingDto) => p.packingId === newPacking.packingId
      );

      if (packing) {
        setFormData((prevState) => ({
          ...prevState,
          packings: [
            ...prevState.packings,
            {
              ...packing,
              quantity: newPacking.quantity,
            } as PackingDto,
          ],
        }));
      }
    }
    setNewPacking({});
    setEditIndex(null);
    onPackingsClose();
  };

  const handleEditIngredient = (index: number) => {
    const ingredient = formData.ingredients[index];
    setNewIngredient(ingredient);
    setEditIndex(index);
    onIngredientsOpen();
  };

  const handleEditPacking = (index: number) => {
    const packing = formData.packings[index];
    setNewPacking(packing);
    setEditIndex(index);
    onPackingsOpen();
  };

  const getQuantityInputStyle = (item: number) => {
    if (!item || item <= 0) {
      return { borderColor: "red", borderWidth: "2px" };
    }
    return {};
  };

  return (
    <>
      <Box
        maxW="1024px"
        mx="auto"
        mt="20px"
        textAlign="center">
        <Text
          as="h1"
          fontSize="2xl"
          mb={4}>
          {formData.id ? "Editar" : "Adicionar"} Receita
        </Text>
      </Box>
      <Box
        maxW="1024px"
        mx="auto"
        mt="10px"
        p={4}
        borderWidth={1}
        borderRadius="lg"
        boxShadow="lg">
        {formData.id && (
          <FormControl mb={4}>
            <Input
              name="id"
              value={formData.id}
              hidden={true}
              readOnly={true}
              variant="filled"
            />
          </FormControl>
        )}
        <FormControl mb={4}>
          <FormLabel>Nome</FormLabel>
          <Input
            name="name"
            value={formData.name}
            onChange={handleChange}
          />
        </FormControl>
        <FormControl mb={4}>
          <FormLabel>Descrição</FormLabel>
          <Input
            name="description"
            value={formData.description}
            onChange={handleChange}
          />
        </FormControl>
        <FormControl mb={4}>
          <FormLabel>Quantidade Produzida</FormLabel>
          <Input
            name="quantity"
            type="number"
            value={formData.quantity}
            onChange={handleChange}
          />
        </FormControl>
        <FormControl mb={4}>
          <FormLabel>Preço de Venda</FormLabel>
          <Input
            name="sellingValue"
            type="number"
            value={formData.sellingValue}
            onChange={handleChange}
          />
        </FormControl>
        <FormControl mb={4}>
          <FormLabel>Materia Prima</FormLabel>
          <Wrap>
            {formData.ingredients.map((ingredient, index) => (
              <WrapItem key={index}>
                <Tag
                  cursor="pointer"
                  onClick={() => handleEditIngredient(index)}
                  colorScheme="green">
                  <TagLabel>{ingredient.productName}</TagLabel>
                  <TagCloseButton
                    onClick={(e) =>
                      handleRemoveIngredient(ingredient, e)
                    }
                  />
                </Tag>
              </WrapItem>
            ))}
          </Wrap>
          <Button
            mt={2}
            onClick={() => {
              fetchIngredients();
              onIngredientsOpen();
            }}>
            Buscar Materia Prima
          </Button>
        </FormControl>
        <FormControl mb={4}>
          <FormLabel>Embalagens</FormLabel>
          <Wrap>
            {formData.packings.map((packing, index) => (
              <WrapItem key={index}>
                <Tag
                  cursor="pointer"
                  onClick={() => handleEditPacking(index)}
                  colorScheme="green">
                  <TagLabel>{packing.packingName}</TagLabel>
                  <TagCloseButton
                    onClick={(e) => handleRemovePacking(packing, e)}
                  />
                </Tag>
              </WrapItem>
            ))}
          </Wrap>
          <Button
            mt={2}
            onClick={() => {
              fetchPackings();
              onPackingsOpen();
            }}>
            Buscar Embalagem
          </Button>
        </FormControl>
        <Button
          mt={3}
          colorScheme="blue"
          onClick={handleSubmit}>
          {formData.id ? "Editar" : "Adicionar"}
        </Button>

        <Modal
          isOpen={isIngredientsOpen}
          onClose={onIngredientsClose}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Buscar Materia Prima</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <Input
                placeholder="Buscar..."
                value={searchTerm}
                onChange={handleSearchChange}
              />
              <Wrap mt={4}>
                {availableIngredients
                  .filter((ingredient) =>
                    ingredient.productName
                      .toLowerCase()
                      .includes(searchTerm.toLowerCase())
                  )
                  .map((ingredient, index) => (
                    <WrapItem key={index}>
                      <Tag
                        cursor="pointer"
                        onClick={() =>
                          setNewIngredient({
                            productName: ingredient.productName,
                            ingredientPrice:
                              ingredient.ingredientPrice,
                            productId: ingredient.productId,
                          })
                        }
                        variant={
                          newIngredient.productName ===
                          ingredient.productName
                            ? "solid"
                            : "subtle"
                        }
                        colorScheme={
                          newIngredient.productName ===
                          ingredient.productName
                            ? "blue"
                            : "gray"
                        }>
                        <TagLabel>{ingredient.productName}</TagLabel>
                      </Tag>
                    </WrapItem>
                  ))}
              </Wrap>
              {newIngredient.productName && (
                <Input
                  placeholder="Quantidade"
                  name="quantity"
                  type="number"
                  value={newIngredient.quantity ?? 0}
                  onChange={handleNewIngredientChange}
                  mt={4}
                  style={getQuantityInputStyle(
                    newIngredient?.quantity ?? 0
                  )}
                />
              )}
            </ModalBody>
            <ModalFooter>
              <Button
                colorScheme="blue"
                mr={3}
                onClick={handleAddIngredient}>
                {editIndex !== null ? "Editar" : "Adicionar"} Materia
                Prima
              </Button>
              <Button
                variant="ghost"
                onClick={onIngredientsClose}>
                Cancelar
              </Button>
            </ModalFooter>
          </ModalContent>
        </Modal>

        <Modal
          isOpen={isPackingsOpen}
          onClose={onPackingsClose}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Embalagens</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <Input
                placeholder="Buscar..."
                value={searchTerm}
                onChange={handleSearchChange}
              />
              <Wrap mt={4}>
                {availablePackings
                  .filter((packing) =>
                    packing.packingName
                      .toLowerCase()
                      .includes(searchTerm.toLowerCase())
                  )
                  .map((packing, index) => (
                    <WrapItem key={index}>
                      <Tag
                        cursor="pointer"
                        onClick={() =>
                          setNewPacking({
                            packingName: packing.packingName,
                            packingUnitPrice:
                              packing.packingUnitPrice,
                            packingId: packing.packingId,
                          })
                        }
                        variant={
                          newPacking.packingName ===
                          packing.packingName
                            ? "solid"
                            : "subtle"
                        }
                        colorScheme={
                          newPacking.packingName ===
                          packing.packingName
                            ? "blue"
                            : "gray"
                        }>
                        <TagLabel>{packing.packingName}</TagLabel>
                      </Tag>
                    </WrapItem>
                  ))}
              </Wrap>
              {newPacking.packingName && (
                <Input
                  placeholder="Quantidade"
                  name="quantity"
                  type="number"
                  value={newPacking.quantity ?? 0}
                  onChange={handleNewPackingChange}
                  mt={4}
                  style={getQuantityInputStyle(
                    newPacking?.quantity ?? 0
                  )}
                />
              )}
            </ModalBody>
            <ModalFooter>
              <Button
                colorScheme="blue"
                mr={3}
                onClick={handleAddPacking}>
                {editIndex !== null ? "Editar" : "Adicionar"}{" "}
                Embalagem
              </Button>
              <Button
                variant="ghost"
                onClick={onPackingsClose}>
                Cancelar
              </Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </Box>
    </>
  );
};

export default RecipeForm;
