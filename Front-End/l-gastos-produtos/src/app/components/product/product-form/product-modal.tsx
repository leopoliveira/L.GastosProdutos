import { UnitOfMeasure } from '@/common/enums/unit-of-measure.enum';
import { IReadProduct } from '@/common/interfaces/product/IReadProduct';
import ProductService from '@/common/services/product';
import { getEnumStrings } from '@/common/utils/utils';
import React, { useEffect, useState } from 'react';
import { X } from 'lucide-react';
import { toast } from 'sonner';

type ProductModalProps = {
  isOpen: boolean;
  onClose: () => void;
  product: IReadProduct | null;
  onSubmit: (acao: string) => void;
};

const ProductModal: React.FC<ProductModalProps> = ({ isOpen, onClose, product, onSubmit }) => {
  const [formData, setFormData] = useState<IReadProduct>({
    id: '',
    name: '',
    quantity: 0,
    price: 0,
    unitOfMeasure: 0,
    unitPrice: 0,
  });

  useEffect(() => {
    if (product) {
      setFormData(product);
    } else {
      setFormData({
        id: '',
        name: '',
        quantity: 0,
        price: 0,
        unitOfMeasure: 0,
        unitPrice: 0,
      });
    }
  }, [product]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    if (name === 'quantity' || name === 'price') {
      const num = parseFloat(value);
      setFormData({ ...formData, [name]: isNaN(num) ? 0 : num });
      return;
    }
    if (name === 'unitOfMeasure') {
      const num = parseInt(value, 10);
      setFormData({ ...formData, unitOfMeasure: isNaN(num) ? 0 : num });
      return;
    }
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const isInvalid =
      formData.name.trim() === '' || formData.quantity <= 0 || formData.price <= 0;

    if (isInvalid) {
      toast.error('Preencha todos os campos obrigatórios.');
      return;
    }

    if (formData.id) {
      ProductService.UpdateProduct(formData.id, formData).then(() => {
        onSubmit('salvo');
      });
    } else {
      ProductService.CreateProduct(formData).then(() => {
        onSubmit('criado');
      });
    }
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none bg-black/50"
      onMouseDown={(e) => {
        if (e.target === e.currentTarget) {
          onClose();
        }
      }}
    >
      <div className="relative w-full max-w-md mx-auto my-6 px-4">
        <div className="relative flex flex-col w-full bg-white border-0 rounded-lg shadow-lg outline-none focus:outline-none">
          {/* Header */}
          <div className="flex items-start justify-between p-5 border-b border-solid border-gray-200 rounded-t">
            <h3 className="text-xl font-semibold">
              {formData.id ? 'Editar Materia Prima' : 'Adicionar Materia Prima'}
            </h3>
            <button
              className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
              onClick={onClose}
            >
              <X className="w-6 h-6 text-gray-500 hover:text-gray-700" />
            </button>
          </div>
          
          {/* Body */}
          <div className="relative p-6 flex-auto">
            <form onSubmit={handleSubmit} className="flex flex-col gap-4">
              {formData.id && (
                <input name="id" value={formData.id} type="hidden" />
              )}
              
              <div>
                <label className="block text-gray-700 text-sm font-bold mb-2">Nome</label>
                <input
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                  required
                  className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline focus:ring-2 focus:ring-blue-500"
                />
              </div>

              <div>
                <label className="block text-gray-700 text-sm font-bold mb-2">Quantidade</label>
                <input
                  name="quantity"
                  type="number"
                  value={formData.quantity === 0 ? '' : formData.quantity}
                  onChange={handleChange}
                  required
                  className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline focus:ring-2 focus:ring-blue-500"
                />
              </div>

              <div>
                <label className="block text-gray-700 text-sm font-bold mb-2">Preço</label>
                <input
                  name="price"
                  type="number"
                  value={formData.price === 0 ? '' : formData.price}
                  onChange={handleChange}
                  required
                  className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline focus:ring-2 focus:ring-blue-500"
                />
              </div>

              <div>
                <label className="block text-gray-700 text-sm font-bold mb-2">Unidade de Medida</label>
                <select
                  name="unitOfMeasure"
                  value={formData.unitOfMeasure}
                  onChange={handleChange}
                  required
                  className="shadow border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline focus:ring-2 focus:ring-blue-500 bg-white"
                >
                  {getEnumStrings(UnitOfMeasure).map((unit, index) => (
                    <option key={index} value={UnitOfMeasure[unit as keyof typeof UnitOfMeasure]}>
                      {unit}
                    </option>
                  ))}
                </select>
              </div>

              <div>
                <label className="block text-gray-700 text-sm font-bold mb-2">Preço Unitário</label>
                <input
                  disabled={true}
                  type="number"
                  value={
                    formData.price === 0 || formData.quantity === 0
                      ? ''
                      : (formData.price / formData.quantity).toFixed(2)
                  }
                  className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-500 bg-gray-100 leading-tight focus:outline-none focus:shadow-outline"
                />
              </div>
            </form>
          </div>

          {/* Footer */}
          <div className="flex items-center justify-end p-6 border-t border-solid border-gray-200 rounded-b gap-2">
            <button
              className="text-gray-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 hover:text-gray-700"
              type="button"
              onClick={onClose}
            >
              Cancelar
            </button>
            <button
              className="bg-blue-500 text-white active:bg-blue-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 hover:bg-blue-600"
              type="button"
              onClick={handleSubmit}
            >
              {formData.id ? 'Atualizar' : 'Criar'}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductModal;
