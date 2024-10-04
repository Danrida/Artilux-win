import sys

# Entry point
if __name__ == "__main__":
    
    # Get arguments passed from Windows forms
    Model_Name = sys.argv[1].strip() # EVAKA HOME PRO
    Unit_Specs = sys.argv[2].strip() # 22kW, 3phase, 32A, IP54, IK08
    Unit_Color = sys.argv[3].strip() # Black
    Ser_Number = sys.argv[4].strip() # 800001252
    Prod_Date = sys.argv[5].strip() # 2024 08
    Barcode = sys.argv[6].strip() # 4779026723380
   
    # Printer settings
    print("SIZE 100mm ,99mm")
    print("GAP 3 mm ,0")
    print("DIRECTION 1")
    print("CLS")

    print("BOX 30,5,1180,1150,4,20") # Border

    print("PUTBMP 200,40,\"EVAKA_LOGO.BMP\"")

    print(f"TEXT 70,300,\"0\",0,18,18,\"{Model_Name}\"")
    print(f"TEXT 70,380,\"0\",0,18,18,\"{Unit_Specs}\"")
    print(f"TEXT 70,460,\"0\",0,18,18,\"Color: {Unit_Color}\"")

    print(f"TEXT 70,560,\"LUCIDA.TTF\",0,16,16,\"Serial number: {Ser_Number}\"")
    print(f"TEXT 70,640,\"LUCIDA.TTF\",0,16,16,\"Production date: {Prod_Date}\"")

    print(f"BARCODE 220,740, \"EAN13\",180,2,0,8,8,\"{Barcode}\"")

    print(f"TEXT 70,1030,\"LUCIDA.TTF\",0,12,12,\"Designed by Evaka      Produced in Lithuania\"")

    print("PRINT 1") # Print