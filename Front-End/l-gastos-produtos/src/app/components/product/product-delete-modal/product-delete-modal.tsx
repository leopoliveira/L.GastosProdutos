import ProductService from "@/common/services/product";
import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
  Button,
} from "@chakra-ui/react";

type ProductDeleteModalProps = {
  phrase: string;
  btnConfirmLabel: string;
  btnCancelLabel: string;
  productId: string;
  isOpen: boolean;
  onConfirm: () => void;
  onClose: () => void;
};

const ProductDeleteModal: React.FC<ProductDeleteModalProps> = ({
  phrase,
  btnConfirmLabel,
  btnCancelLabel,
  productId,
  isOpen,
  onConfirm,
  onClose,
}) => {
  const handleConfirm = () => {
    ProductService.DeleteProduct(productId);
    onConfirm();
  };

  return (
    <Modal
      isOpen={isOpen}
      onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Atenção</ModalHeader>
        <ModalCloseButton />
        <ModalBody>{phrase}</ModalBody>
        <ModalFooter>
          <Button
            colorScheme="red"
            mr={3}
            onClick={handleConfirm}>
            {btnConfirmLabel}
          </Button>
          <Button
            variant="ghost"
            onClick={onClose}>
            {btnCancelLabel}
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default ProductDeleteModal;
