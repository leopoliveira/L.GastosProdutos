import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import IngredientDto from '@/common/interfaces/recipe/dtos/IngredientDto';
import PackingDto from '@/common/interfaces/recipe/dtos/PackingDto';
import PackingService from '@/common/services/packing';
import ProductService from '@/common/services/product';
import RecipeService from '@/common/services/recipe';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import { toast } from 'sonner';
import { X, Search } from 'lucide-react';

type RecipeFormProps = {
  recipe: IReadRecipe | null;
  onFormSubmit: () => void;
};

const RecipeForm: React.FC<RecipeFormProps> = ({ recipe, onFormSubmit }) => {
  const [isIngredientsOpen, setIsIngredientsOpen] = useState(false);
  const [isPackingsOpen, setIsPackingsOpen] = useState(false);

  const [formData, setFormData] = useState<IReadRecipe>({
    id: '',
    name: '',
    description: '',
    totalCost: 0,
    ingredients: [],
    packings: [],
    quantity: 0,
    sellingValue: 0,
  });
  const [searchTerm, setSearchTerm] = useState('');
  const [availableIngredients, setAvailableIngredients] = useState<IngredientDto[]>([]);
  const [availablePackings, setAvailablePackings] = useState<PackingDto[]>([]);
  const [newIngredient, setNewIngredient] = useState<Partial<IngredientDto>>({});
  const [newPacking, setNewPacking] = useState<Partial<PackingDto>>({});
  const [editIndex, setEditIndex] = useState<number | null>(null);
  const router = useRouter();

  useEffect(() => {
    if (recipe) {
      setFormData(recipe);
    } else {
      setFormData({
        id: '',
        name: '',
        description: '',
        totalCost: 0,
        ingredients: [],
        packings: [],
        quantity: 0,
        sellingValue: 0,
      });
    }
  }, [recipe]);

  const handleRemoveIngredient = (ingredient: IngredientDto, e: React.MouseEvent) => {
    e.stopPropagation();
    setFormData((prevState) => ({
      ...prevState,
      ingredients: prevState.ingredients.filter((i) => i !== ingredient),
    }));
  };

  const handleRemovePacking = (packing: PackingDto, e: React.MouseEvent) => {
    e.stopPropagation();
    setFormData((prevState) => ({
      ...prevState,
      packings: prevState.packings.filter((p) => p !== packing),
    }));
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(e.target.value);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (formData.name === '' || formData.ingredients.length === 0) {
      toast.error('Preencha todos os campos');
      return;
    }

    if (formData.id) {
      RecipeService.UpdateRecipe(formData.id, formData).then(() => {
        router.push('/recipes');
      });
    } else {
      RecipeService.CreateRecipe(formData).then(() => {
        router.push('/recipes');
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

  const handleNewIngredientChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setNewIngredient((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleNewPackingChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setNewPacking((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleAddIngredient = () => {
    if (!newIngredient.quantity || newIngredient.quantity <= 0) {
      alert('Quantidade deve ser maior que 0.');
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
    setIsIngredientsOpen(false);
  };

  const handleAddPacking = () => {
    if (!newPacking.quantity || newPacking.quantity <= 0) {
      alert('Quantidade deve ser maior que 0.');
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
    setIsPackingsOpen(false);
  };

  const handleEditIngredient = (index: number) => {
    const ingredient = formData.ingredients[index];
    setNewIngredient(ingredient);
    setEditIndex(index);
    setIsIngredientsOpen(true);
  };

  const handleEditPacking = (index: number) => {
    const packing = formData.packings[index];
    setNewPacking(packing);
    setEditIndex(index);
    setIsPackingsOpen(true);
  };

  const getQuantityInputStyle = (item: number) => {
    if (!item || item <= 0) {
      return "border-red-500 border-2";
    }
    return "";
  };

  return (
    <>
      <div className="max-w-4xl mx-auto mt-5 text-center">
        <h1 className="text-2xl font-bold mb-4">
          {formData.id ? 'Editar' : 'Adicionar'} Receita
        </h1>
      </div>
      <div className="max-w-4xl mx-auto mt-2 p-4 border border-gray-200 rounded-lg shadow-lg bg-white">
        {formData.id && (
          <input name="id" value={formData.id} type="hidden" />
        )}
        
        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2">Nome</label>
          <input
            name="name"
            value={formData.name}
            onChange={handleChange}
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2">Descrição</label>
          <input
            name="description"
            value={formData.description}
            onChange={handleChange}
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2">Quantidade Produzida</label>
          <input
            name="quantity"
            type="number"
            value={formData.quantity}
            onChange={handleChange}
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2">Preço de Venda da Unidade</label>
          <input
            name="sellingValue"
            type="number"
            value={formData.sellingValue}
            onChange={handleChange}
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2">Materia Prima</label>
          <div className="flex flex-wrap gap-2 mb-2">
            {formData.ingredients.map((ingredient, index) => (
              <div
                key={index}
                className="flex items-center bg-green-100 text-green-800 text-sm font-medium px-2.5 py-0.5 rounded cursor-pointer hover:bg-green-200"
                onClick={() => handleEditIngredient(index)}
              >
                <span>{ingredient.productName}</span>
                <button
                  type="button"
                  className="ml-2 text-green-900 hover:text-green-950"
                  onClick={(e) => handleRemoveIngredient(ingredient, e)}
                >
                  <X size={14} />
                </button>
              </div>
            ))}
          </div>
          <button
            type="button"
            className="mt-2 bg-gray-200 hover:bg-gray-300 text-gray-800 font-semibold py-2 px-4 rounded inline-flex items-center"
            onClick={() => {
              fetchIngredients();
              setIsIngredientsOpen(true);
            }}
          >
            <Search size={16} className="mr-2" />
            Buscar Materia Prima
          </button>
        </div>

        <div className="mb-4">
          <label className="block text-gray-700 text-sm font-bold mb-2">Embalagens</label>
          <div className="flex flex-wrap gap-2 mb-2">
            {formData.packings.map((packing, index) => (
              <div
                key={index}
                className="flex items-center bg-green-100 text-green-800 text-sm font-medium px-2.5 py-0.5 rounded cursor-pointer hover:bg-green-200"
                onClick={() => handleEditPacking(index)}
              >
                <span>{packing.packingName}</span>
                <button
                  type="button"
                  className="ml-2 text-green-900 hover:text-green-950"
                  onClick={(e) => handleRemovePacking(packing, e)}
                >
                  <X size={14} />
                </button>
              </div>
            ))}
          </div>
          <button
            type="button"
            className="mt-2 bg-gray-200 hover:bg-gray-300 text-gray-800 font-semibold py-2 px-4 rounded inline-flex items-center"
            onClick={() => {
              fetchPackings();
              setIsPackingsOpen(true);
            }}
          >
            <Search size={16} className="mr-2" />
            Buscar Embalagem
          </button>
        </div>

        <button
          className="mt-3 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded w-full"
          onClick={handleSubmit}
        >
          {formData.id ? 'Editar' : 'Adicionar'}
        </button>

        {/* Ingredients Modal */}
        {isIngredientsOpen && (
          <div className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none bg-black/50">
            <div className="relative w-full max-w-lg mx-auto my-6 px-4">
              <div className="relative flex flex-col w-full bg-white border-0 rounded-lg shadow-lg outline-none focus:outline-none">
                <div className="flex items-start justify-between p-5 border-b border-solid border-gray-200 rounded-t">
                  <h3 className="text-xl font-semibold">Buscar Materia Prima</h3>
                  <button
                    className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                    onClick={() => setIsIngredientsOpen(false)}
                  >
                    <X className="w-6 h-6 text-gray-500 hover:text-gray-700" />
                  </button>
                </div>
                <div className="relative p-6 flex-auto">
                  <input
                    placeholder="Buscar..."
                    value={searchTerm}
                    onChange={handleSearchChange}
                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"
                  />
                  <div className="flex flex-wrap gap-2 max-h-60 overflow-y-auto">
                    {availableIngredients
                      .filter((ingredient) =>
                        ingredient.productName.toLowerCase().includes(searchTerm.toLowerCase())
                      )
                      .map((ingredient, index) => (
                        <div
                          key={index}
                          onClick={() =>
                            setNewIngredient({
                              productName: ingredient.productName,
                              ingredientPrice: ingredient.ingredientPrice,
                              productId: ingredient.productId,
                            })
                          }
                          className={`cursor-pointer px-3 py-1 rounded-full text-sm font-semibold ${
                            newIngredient.productName === ingredient.productName
                              ? 'bg-blue-500 text-white'
                              : 'bg-gray-200 text-gray-700 hover:bg-gray-300'
                          }`}
                        >
                          {ingredient.productName}
                        </div>
                      ))}
                  </div>
                  {newIngredient.productName && (
                    <input
                      placeholder="Quantidade"
                      name="quantity"
                      type="number"
                      value={newIngredient.quantity ?? 0}
                      onChange={handleNewIngredientChange}
                      className={`mt-4 shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline ${getQuantityInputStyle(newIngredient?.quantity ?? 0)}`}
                    />
                  )}
                </div>
                <div className="flex items-center justify-end p-6 border-t border-solid border-gray-200 rounded-b gap-2">
                  <button
                    className="text-gray-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 hover:text-gray-700"
                    type="button"
                    onClick={() => setIsIngredientsOpen(false)}
                  >
                    Cancelar
                  </button>
                  <button
                    className="bg-blue-500 text-white active:bg-blue-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 hover:bg-blue-600"
                    type="button"
                    onClick={handleAddIngredient}
                  >
                    {editIndex !== null ? 'Editar' : 'Adicionar'} Materia Prima
                  </button>
                </div>
              </div>
            </div>
          </div>
        )}

        {/* Packings Modal */}
        {isPackingsOpen && (
          <div className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none bg-black/50">
            <div className="relative w-full max-w-lg mx-auto my-6 px-4">
              <div className="relative flex flex-col w-full bg-white border-0 rounded-lg shadow-lg outline-none focus:outline-none">
                <div className="flex items-start justify-between p-5 border-b border-solid border-gray-200 rounded-t">
                  <h3 className="text-xl font-semibold">Embalagens</h3>
                  <button
                    className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                    onClick={() => setIsPackingsOpen(false)}
                  >
                    <X className="w-6 h-6 text-gray-500 hover:text-gray-700" />
                  </button>
                </div>
                <div className="relative p-6 flex-auto">
                  <input
                    placeholder="Buscar..."
                    value={searchTerm}
                    onChange={handleSearchChange}
                    className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-4"
                  />
                  <div className="flex flex-wrap gap-2 max-h-60 overflow-y-auto">
                    {availablePackings
                      .filter((packing) =>
                        packing.packingName.toLowerCase().includes(searchTerm.toLowerCase())
                      )
                      .map((packing, index) => (
                        <div
                          key={index}
                          onClick={() =>
                            setNewPacking({
                              packingName: packing.packingName,
                              packingUnitPrice: packing.packingUnitPrice,
                              packingId: packing.packingId,
                            })
                          }
                          className={`cursor-pointer px-3 py-1 rounded-full text-sm font-semibold ${
                            newPacking.packingName === packing.packingName
                              ? 'bg-blue-500 text-white'
                              : 'bg-gray-200 text-gray-700 hover:bg-gray-300'
                          }`}
                        >
                          {packing.packingName}
                        </div>
                      ))}
                  </div>
                  {newPacking.packingName && (
                    <input
                      placeholder="Quantidade"
                      name="quantity"
                      type="number"
                      value={newPacking.quantity ?? 0}
                      onChange={handleNewPackingChange}
                      className={`mt-4 shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline ${getQuantityInputStyle(newPacking?.quantity ?? 0)}`}
                    />
                  )}
                </div>
                <div className="flex items-center justify-end p-6 border-t border-solid border-gray-200 rounded-b gap-2">
                  <button
                    className="text-gray-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 hover:text-gray-700"
                    type="button"
                    onClick={() => setIsPackingsOpen(false)}
                  >
                    Cancelar
                  </button>
                  <button
                    className="bg-blue-500 text-white active:bg-blue-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 hover:bg-blue-600"
                    type="button"
                    onClick={handleAddPacking}
                  >
                    {editIndex !== null ? 'Editar' : 'Adicionar'} Embalagem
                  </button>
                </div>
              </div>
            </div>
          </div>
        )}
      </div>
    </>
  );
};

export default RecipeForm;
