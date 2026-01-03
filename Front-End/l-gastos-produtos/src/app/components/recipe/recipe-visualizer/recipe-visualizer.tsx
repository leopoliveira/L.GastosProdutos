import IReadRecipe from '@/common/interfaces/recipe/IReadRecipe';
import { formatCurrency } from '@/common/services/utils/utils';
import { useRouter } from 'next/navigation';

type RecipeViewProps = {
  recipe: IReadRecipe;
};

const RecipeVisualizer = ({ recipe }: RecipeViewProps) => {
  const router = useRouter();

  const handleBackClick = () => {
    router.push('/recipes');
  };

  return (
    <>
      <div className="max-w-4xl mx-auto mt-5 p-4 border border-gray-200 rounded-lg shadow-lg bg-white">
        <h1 className="text-2xl font-bold mb-4 text-center">
          {recipe.name}
        </h1>
        <div className="flex flex-col gap-4">
          {recipe.description && (
            <div className="flex gap-5 items-center">
              <span className="font-bold">Descrição:</span>
              <span>{recipe.description ?? 'Sem descrição'}</span>
            </div>
          )}
          <hr className="border-gray-200" />
          <div className="flex flex-wrap gap-5 items-center">
            <span className="font-bold text-red-600">
              Custo Total:
            </span>
            <span className="text-red-600">{formatCurrency(recipe.totalCost)}</span>
            <div className="h-12 w-px bg-gray-200 mx-2 hidden sm:block"></div>
            <span className="font-bold">Quantidade Produzida:</span>
            <span>{recipe.quantity}</span>
            <div className="h-12 w-px bg-gray-200 mx-2 hidden sm:block"></div>
            <span className="font-bold">Custo por Unidade:</span>
            <span>{formatCurrency(recipe.totalCost / recipe.quantity)}</span>
          </div>
          <hr className="border-gray-200" />
          <div className="flex flex-wrap gap-5 items-center">
            <span className="font-bold">Preço de Venda:</span>
            <span>{formatCurrency(recipe.sellingValue)}</span>
            <div className="h-12 w-px bg-gray-200 mx-2 hidden sm:block"></div>
            <span className="font-bold">Lucro por Unidade:</span>
            <span>{formatCurrency(recipe.sellingValue - recipe.totalCost / recipe.quantity)}</span>
            <div className="h-12 w-px bg-gray-200 mx-2 hidden sm:block"></div>
            <span className="font-bold text-green-600">
              Lucro Total:
            </span>
            <span className="text-green-600">
              {formatCurrency(recipe.sellingValue * recipe.quantity - recipe.totalCost)}
            </span>
          </div>
          <hr className="border-gray-200" />
          <div className="w-full">
            <p className="font-bold mb-2">
              Ingredientes:
            </p>
            <div className="flex flex-wrap gap-4">
              {recipe.ingredients.map((ingredient, index) => (
                <div key={index} className="min-w-[200px] max-w-[500px] p-4 border border-gray-200 rounded-lg shadow-md w-full bg-green-100 text-green-800">
                  <p className="font-bold">{ingredient.productName}</p>
                  <p>Quantidade: {ingredient.quantity}</p>
                  <p>Preço: {formatCurrency(ingredient.ingredientPrice)}</p>
                  <p className="font-bold">
                    Custo: {formatCurrency(ingredient.ingredientPrice * ingredient.quantity)}
                  </p>
                </div>
              ))}
            </div>
          </div>
          <hr className="border-gray-200" />
          <div className="w-full">
            <p className="font-bold mb-2">
              Embalagens:
            </p>
            <div className="flex flex-wrap gap-4">
              {recipe.packings.map((packing, index) => (
                <div key={index} className="min-w-[200px] max-w-[500px] p-4 border border-gray-200 rounded-lg shadow-md w-full bg-green-100 text-green-800">
                  <p className="font-bold">{packing.packingName}</p>
                  <p>Quantidade: {packing.quantity}</p>
                  <p>Preço: {formatCurrency(packing.packingUnitPrice)}</p>
                  <p className="font-bold">
                    Custo: {formatCurrency(packing.packingUnitPrice * packing.quantity)}
                  </p>
                </div>
              ))}
            </div>
          </div>
          <hr className="border-gray-200" />
        </div>
      </div>
      <div className="max-w-4xl mx-auto mt-5 text-right p-4">
        <button 
          className="w-[150px] bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          onClick={handleBackClick}
        >
          Voltar
        </button>
      </div>
    </>
  );
};

export default RecipeVisualizer;
